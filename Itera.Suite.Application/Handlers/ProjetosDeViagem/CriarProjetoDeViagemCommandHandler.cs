using Itera.Suite.Application.Commands.ProjetosDeViagem;
using Itera.Suite.Domain.Entities;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Shared.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Itera.Suite.Application.Handlers.ProjetosDeViagem;

public class CriarProjetoDeViagemCommandHandler
{
    private readonly IProjetoDeViagemRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CriarProjetoDeViagemCommandHandler(
        IProjetoDeViagemRepository repository,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Guid> HandleAsync(CriarProjetoDeViagemCommand command)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // Para AutorId -> se UserId válido, usa o Guid real
        Guid autorId = Guid.Empty;
        if (Guid.TryParse(userId, out var parsedId))
            autorId = parsedId;

        var criadoPor = string.IsNullOrWhiteSpace(userId) ? "Sistema" : userId;

        var tipoProjeto = Enum.Parse<TipoProjeto>(command.Tipo, true);

        var projeto = new ProjetoDeViagem(
            clienteId: command.ClienteId,
            nomeInterno: command.NomeInterno,
            origem: command.Origem,
            destino: command.Destino,
            objetivo: command.Objetivo,
            dataSaida: DateOnly.FromDateTime(command.DataSaida),
            dataRetorno: DateOnly.FromDateTime(command.DataRetorno),
            tipo: tipoProjeto,
            criadoPor: criadoPor
        );

        if (!string.IsNullOrWhiteSpace(command.ObservacaoInicial))
        {
            projeto.AdicionarObservacao(command.ObservacaoInicial, autorId);
        }

        await _repository.AdicionarAsync(projeto);
        await _repository.SalvarAlteracoesAsync();

        return projeto.Id;
    }

}
