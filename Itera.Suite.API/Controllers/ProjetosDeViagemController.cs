using Itera.Suite.Application.Commands.ProjetosDeViagem;
using Itera.Suite.Application.DTOs;
using Itera.Suite.Application.Handlers.ProjetosDeViagem;
using Itera.Suite.Application.Interfaces;
using Itera.Suite.Application.Queries.ProjetosDeViagem;
using Itera.Suite.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class ProjetosDeViagemController : ControllerBase
{
    private readonly CriarProjetoDeViagemCommandHandler _criarHandler;
    private readonly AtualizarProjetoDeViagemCommandHandler _atualizarHandler;
    private readonly IProjetoDeViagemRepository _repository; // Entity Commands
    private readonly IProjetoDeViagemQuery _query;           // DTO Query

    public ProjetosDeViagemController(
        CriarProjetoDeViagemCommandHandler criarHandler,
        AtualizarProjetoDeViagemCommandHandler atualizarHandler,
        IProjetoDeViagemRepository repository,
        IProjetoDeViagemQuery query)
    {
        _criarHandler = criarHandler;
        _atualizarHandler = atualizarHandler;
        _repository = repository;
        _query = query;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CriarProjetoDeViagemCommand command)
    {
        var id = await _criarHandler.HandleAsync(command);
        return CreatedAtAction(nameof(GetById), new { id }, new { id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, AtualizarProjetoDeViagemCommand command)
    {
        if (id != command.Id)
            return BadRequest("Id da rota não confere com o payload.");

        await _atualizarHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var projetos = await _query.ListarTodosAsync();
        return Ok(projetos);
    }

    // ✅ GET BY ID: Details — SEM MediatR — usando ObterPorIdComItensAsync
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var projeto = await _repository.ObterPorIdComItensAsync(id);
        if (projeto == null) return NotFound();

        var dto = new ProjetoDeViagemDetailsDto
        {
            Id = projeto.Id,
            NomeInterno = projeto.NomeInterno,
            Origem = projeto.Origem,
            Destino = projeto.Destino,
            Objetivo = projeto.Objetivo,
            Tipo = projeto.Tipo.ToString(),
            Status = projeto.Status.ToString(),
            DataSaida = projeto.DataSaida,
            DataRetorno = projeto.DataRetorno,
            ClienteNome = projeto.Cliente.Nome,
            ValorTotalProvisionado = projeto.CalcularValorTotalProvisionado(),
            ItensDeCusto = projeto.ItensDeCusto?
                .Select(i => new ItemDeCustoDto
                {
                    Id = i.Id,
                    Descricao = i.Descricao,
                    FornecedorNome = i.Fornecedor.Nome,
                    ValorTotal = i.Total,
                    Status = i.StatusAtual.ToString()
                })
                .ToList() ?? new List<ItemDeCustoDto>()
        };

        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var projeto = await _repository.ObterPorIdAsync(id);
        if (projeto == null) return NotFound();

        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var atualizadoPor = string.IsNullOrWhiteSpace(userId) ? "Sistema" : userId;

        projeto.Inativar(atualizadoPor);

        await _repository.SalvarAlteracoesAsync();

        return NoContent();
    }
}
