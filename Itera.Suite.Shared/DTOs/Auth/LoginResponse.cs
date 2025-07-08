namespace Itera.Suite.Shared.DTOs.Auth;

public class LoginResponse
{
    public bool Sucesso { get; set; }
    public string? Token { get; set; }
    public DateTime? Expiration { get; set; }
    public string? UsuarioId { get; set; }
    public string? Email { get; set; }
    public List<string> Roles { get; set; } = new();
    public IEnumerable<string> Erros { get; set; } = Enumerable.Empty<string>();
}
