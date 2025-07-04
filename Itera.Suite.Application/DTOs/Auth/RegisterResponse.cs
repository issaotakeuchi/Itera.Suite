namespace Itera.Suite.Application.DTOs.Auth;

public class RegisterResponse
{
    public bool Sucesso { get; set; }
    public IEnumerable<string> Erros { get; set; } = Enumerable.Empty<string>();
}
