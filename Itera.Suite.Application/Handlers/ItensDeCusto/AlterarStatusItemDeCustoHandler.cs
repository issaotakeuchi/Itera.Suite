using Itera.Suite.Application.DTOs;
using Itera.Suite.Domain.Interfaces;

namespace Itera.Suite.Application.Handlers.ItensDeCusto;

public class AlterarStatusItemDeCustoHandler
{
    private readonly IItemDeCustoRepository _repo;

    public AlterarStatusItemDeCustoHandler(IItemDeCustoRepository repo)
    {
        _repo = repo;
    }

    public async Task HandleAsync(AlterarStatusItemDeCustoCommand command, string usuario)
    {
        var item = await _repo.ObterPorIdAsync(command.Id);
        if (item == null) throw new Exception("Item não encontrado");

        item.AlterarStatus(command.NovoStatus, usuario, command.Justificativa);
        await _repo.SalvarAlteracoesAsync();
    }
}

