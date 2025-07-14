using Itera.Suite.Application.Commands.ProjetosDeViagem;
using Itera.Suite.Domain.Interfaces;
using Itera.Suite.Shared.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Itera.Suite.Application.Handlers.ProjetosDeViagem;

public class AtualizarProjetoDeViagemCommandHandler
{
    private readonly IProjetoDeViagemRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AtualizarProjetoDeViagemCommandHandler(
        IProjetoDeViagemRepository repository,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task HandleAsync(AtualizarProjetoDeViagemCommand command)
    {
        var projeto = await _repository.ObterPorIdAsync(command.Id)
            ?? throw new InvalidOperationException("Projeto não encontrado.");

        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var atualizadoPor = string.IsNullOrWhiteSpace(userId) ? "Sistema" : userId;

        TipoProjeto? tipo = null;
        if (!string.IsNullOrWhiteSpace(command.Tipo))
            tipo = Enum.Parse<TipoProjeto>(command.Tipo, true);

        projeto.AtualizarDados(
            nomeInterno: command.NomeInterno,
            origem: command.Origem,
            destino: command.Destino,
            objetivo: command.Objetivo,
            dataSaida: command.DataSaida.HasValue ? DateOnly.FromDateTime(command.DataSaida.Value) : null,
            dataRetorno: command.DataRetorno.HasValue ? DateOnly.FromDateTime(command.DataRetorno.Value) : null,
            tipo: tipo,
            atualizadoPor: atualizadoPor
        );

        await _repository.SalvarAlteracoesAsync();
    }
}
