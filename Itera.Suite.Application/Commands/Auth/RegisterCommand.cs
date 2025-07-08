using Itera.Suite.Shared.DTOs.Auth;
using MediatR;

namespace Itera.Suite.Application.Commands.Auth;

public class RegisterCommand : IRequest<RegisterResponse>
{
    public RegisterRequest Dados { get; set; }

    public RegisterCommand(RegisterRequest dados)
    {
        Dados = dados;
    }
}
