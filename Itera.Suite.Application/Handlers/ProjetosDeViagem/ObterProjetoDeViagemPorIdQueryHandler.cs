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

        var baseConfirmada = projeto.Bases.FirstOrDefault(b => b.Confirmada);
        var outrasBases = projeto.Bases.Where(b => !b.Confirmada).ToList();

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
    }
}
