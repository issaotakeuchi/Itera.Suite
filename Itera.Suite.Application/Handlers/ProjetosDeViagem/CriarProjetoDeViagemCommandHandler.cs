using Itera.Suite.Application.Commands.ProjetosDeViagem;
using Itera.Suite.Domain.Entities;
using Itera.Suite.Domain.Enums;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Domain.ValueObjects;

namespace Itera.Suite.Application.Handlers.ProjetosDeViagem;

public class CriarProjetoDeViagemCommandHandler
{
    private readonly IProjetoDeViagemRepository _repository;

    public CriarProjetoDeViagemCommandHandler(IProjetoDeViagemRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> HandleAsync(CriarProjetoDeViagemCommand command)
    {
        var projeto = new ProjetoDeViagem
        {
            Id = Guid.NewGuid(),
            ClienteId = command.ClienteId,
            NomeInterno = command.NomeInterno,
            Origem = command.Origem,
            Destino = command.Destino,
            Objetivo = command.Objetivo,
            DataSaida = command.DataSaida,
            DataRetorno = command.DataRetorno,
            Tipo = Enum.Parse<TipoProjeto>(command.Tipo),
            Status = StatusProjeto.Estimado
        };

        if (!string.IsNullOrWhiteSpace(command.ObservacaoInicial))
        {
            projeto.Observacoes.Add(new ObservacaoProjeto
            {
                DataHora = DateTime.UtcNow,
                Texto = command.ObservacaoInicial,
                Autor = "Sistema"
            });
        }

        await _repository.AdicionarAsync(projeto);
        await _repository.SalvarAlteracoesAsync();

        return projeto.Id;
    }
}
