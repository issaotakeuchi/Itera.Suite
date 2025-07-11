using Itera.Suite.Application.Commands.ProjetosDeViagem;
using Itera.Suite.Domain.Entities;
using Itera.Suite.Domain.Enums;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Shared.Enums;

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
        // ⚙️ Converter DateTime -> DateOnly
        var dataSaida = DateOnly.FromDateTime(command.DataSaida);
        var dataRetorno = DateOnly.FromDateTime(command.DataRetorno);

        // ⚙️ Tenta parsear Enum de forma segura
        if (!Enum.TryParse<TipoProjeto>(command.Tipo, true, out var tipoProjeto))
            throw new ArgumentException($"Tipo de projeto '{command.Tipo}' é inválido.");

        // ⚙️ Definir autor padrão
        const string autorSistema = "Sistema";

        // ⚙️ Cria projeto usando construtor rico
        var projeto = new ProjetoDeViagem(
            clienteId: command.ClienteId,
            nomeInterno: command.NomeInterno,
            origem: command.Origem,
            destino: command.Destino,
            objetivo: command.Objetivo,
            dataSaida: dataSaida,
            dataRetorno: dataRetorno,
            tipo: tipoProjeto,
            criadoPor: autorSistema
        );

        // ⚙️ Usa método do Agregado pra adicionar observação
        if (!string.IsNullOrWhiteSpace(command.ObservacaoInicial))
        {
            projeto.AdicionarObservacao(command.ObservacaoInicial, Guid.Empty);
        }

        await _repository.AdicionarAsync(projeto);
        await _repository.SalvarAlteracoesAsync();

        return projeto.Id;
    }
}
