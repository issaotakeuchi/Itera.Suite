namespace Itera.Suite.Application.DTOs.Auth;

public class RegisterRequest
{
    public string Email { get; set; }
    public string Senha { get; set; }
    public string NomeCompleto { get; set; }
}