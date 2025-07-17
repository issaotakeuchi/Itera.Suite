using MediatR;
using Itera.Suite.Application.DTOs;

namespace Itera.Suite.Application.Queries.ProjetosDeViagem;

public class ObterProjetoDeViagemPorIdQuery : IRequest<ProjetoDeViagemDetailsDto>
{
    public Guid Id { get; set; }

    public ObterProjetoDeViagemPorIdQuery(Guid id)
    {
        Id = id;
    }
}
