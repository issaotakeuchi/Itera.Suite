// Infrastructure/Auth/UsuarioIdentity.cs
using Microsoft.AspNetCore.Identity;

namespace Itera.Suite.Domain.Identity;

public class UsuarioIdentity : IdentityUser<Guid>
{
    public string NomeCompleto { get; set; } = default!;
}

