using Itera.Suite.Application.Commands.BasesDeCalculo;
using Itera.Suite.Application.Commands.ProjetosDeViagem;
using Itera.Suite.Application.DTOs;
using Itera.Suite.Application.Handlers.BasesDeCalculo;
using Itera.Suite.Application.Handlers.ProjetosDeViagem;
using Itera.Suite.Application.Interfaces;
using Itera.Suite.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class ProjetosDeViagemController : ControllerBase
{
    private readonly CriarProjetoDeViagemCommandHandler _criarHandler;
    private readonly AtualizarProjetoDeViagemCommandHandler _atualizarHandler;
    private readonly CriarBaseDeCalculoCommandHandler _criarBaseHandler;
    private readonly DefinirQuantidadesDaBaseCommandHandler _definirQuantidadesHandler;
    private readonly ConfirmarBaseHandler _confirmarBaseHandler;
    private readonly IProjetoDeViagemRepository _repository; // Entity Commands
    private readonly IProjetoDeViagemQuery _query;           // DTO Query

    public ProjetosDeViagemController(
        CriarProjetoDeViagemCommandHandler criarHandler,
        AtualizarProjetoDeViagemCommandHandler atualizarHandler,
        CriarBaseDeCalculoCommandHandler criarBaseHandler,
        DefinirQuantidadesDaBaseCommandHandler definirQuantidadesHandler,
        ConfirmarBaseHandler confirmarBaseHandler,
        IProjetoDeViagemRepository repository,
        IProjetoDeViagemQuery query)
    {
        _criarHandler = criarHandler;
        _atualizarHandler = atualizarHandler;
        _criarBaseHandler = criarBaseHandler;
        _definirQuantidadesHandler = definirQuantidadesHandler;
        _confirmarBaseHandler = confirmarBaseHandler;
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

        var baseConfirmada = projeto.Bases.FirstOrDefault(b => b.Confirmada);
        var outrasBases = projeto.Bases.Where(b => !b.Confirmada).ToList();

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
            ValorTotalProvisionado = baseConfirmada?.TotalComMarkup ?? 0,

            BaseConfirmada = baseConfirmada is not null
                ? new BaseDeCalculoDto
                {
                    Id = baseConfirmada.Id,
                    Nome = baseConfirmada.Nome,
                    Markup = baseConfirmada.Markup,
                    QtdAlunos = baseConfirmada.QtdAlunos,
                    QtdProfessores = baseConfirmada.QtdProfessores,
                    QtdGuias = baseConfirmada.QtdGuias,
                    QtdMotoristas = baseConfirmada.QtdMotoristas,
                    MotoristaPermanece = baseConfirmada.MotoristaPermanece,
                    Total = baseConfirmada.Total,
                    TotalComMarkup = baseConfirmada.TotalComMarkup,
                    Itens = baseConfirmada.Itens.Select(i => new ItemDeCustoAplicadoDto
                    {
                        Id = i.Id,
                        Subtipo = i.Subtipo,
                        QuantidadeTotal = i.QuantidadeTotal,
                        QuantidadeCortesia = i.QuantidadeCortesia,
                        ValorEditado = i.ValorEditado ?? i.ItemDeCusto.ValorPadrao,
                        Total = i.Total,
                        Categoria = i.ItemDeCusto.Categoria.ToString(),
                        Descricao = i.ItemDeCusto.Descricao,
                        Fornecedor = i.ItemDeCusto.Fornecedor?.Nome
                    }).ToList()
                }
                : null,

            OutrasBases = outrasBases.Select(b => new BaseDeCalculoResumoDto
            {
                Id = b.Id,
                Nome = b.Nome,
                Total = b.TotalComMarkup,
                Confirmada = b.Confirmada
            }).ToList()
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

    [HttpPost("bases/{id}")]
    public async Task<IActionResult> PostBase(Guid id, CriarBaseDeCalculoCommand command)
    {
        if (id != command.ProjetoDeViagemId)
            return BadRequest("Id da rota não confere com o payload.");

        await _criarBaseHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpPut("bases/{id}/quantidades")]
    public async Task<IActionResult> DefinirQuantidades(Guid id, DefinirQuantidadesDaBaseCommand command)
    {
        if (id != command.BaseId)
            return BadRequest("Id da rota não confere com o payload.");

        await _definirQuantidadesHandler.HandleAsync(command);
        return NoContent();
    }

    [HttpPut("{projetoId}/bases/{baseId}/confirmar")]
    public async Task<IActionResult> ConfirmarBase(Guid projetoId, Guid baseId)
    {
        await _confirmarBaseHandler.HandleAsync(projetoId, baseId);
        return NoContent();
    }
}
