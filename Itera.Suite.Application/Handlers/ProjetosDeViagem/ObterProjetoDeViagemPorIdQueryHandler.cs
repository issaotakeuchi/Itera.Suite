using Itera.Suite.Application.DTOs;
using Itera.Suite.Application.Queries.ProjetosDeViagem;
using Itera.Suite.Domain.Interfaces;
using MediatR;

namespace Itera.Suite.Application.Handlers.ProjetosDeViagem;

public class ObterProjetoDeViagemPorIdQueryHandler
    : IRequestHandler<ObterProjetoDeViagemPorIdQuery, ProjetoDeViagemDetailsDto>
{
    private readonly IProjetoDeViagemRepository _repository;

    public ObterProjetoDeViagemPorIdQueryHandler(IProjetoDeViagemRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProjetoDeViagemDetailsDto> Handle(
        ObterProjetoDeViagemPorIdQuery request, CancellationToken cancellationToken)
    {
        var projeto = await _repository.ObterPorIdComItensAsync(request.Id)
            ?? throw new InvalidOperationException("Projeto não encontrado.");

        return new ProjetoDeViagemDetailsDto
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
            ItensDeCusto = projeto.ItensDeCusto.Select(item => new ItemDeCustoDto
            {
                Id = item.Id,
                Descricao = item.Descricao,
                FornecedorNome = item.Fornecedor.Nome,
                ValorTotal = item.Total,
                Status = item.StatusAtual.ToString()
            }).ToList()
        };
    }
}
