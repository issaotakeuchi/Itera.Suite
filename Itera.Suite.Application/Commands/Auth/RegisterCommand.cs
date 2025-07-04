using Itera.Suite.Application.DTOs.Auth;
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
