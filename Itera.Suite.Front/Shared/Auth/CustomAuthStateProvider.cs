using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Itera.Suite.Front.Shared.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;

    public CustomAuthStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        var identity = new ClaimsIdentity();

        if (!string.IsNullOrWhiteSpace(token))
        {
            try
            {
                if (token.Count(c => c == '.') != 2)
                {
                    Console.WriteLine("⚠️ Token não tem formato JWT. Ignorando e removendo.");
                    await _localStorage.RemoveItemAsync("authToken");
                    token = null;
                }
                else
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(token);

                    // ✅ Verifica expiração
                    if (jwtToken.ValidTo < DateTime.UtcNow)
                    {
                        Console.WriteLine("⚠️ Token expirado. Removendo.");
                        await _localStorage.RemoveItemAsync("authToken");
                        token = null;
                    }
                    else
                    {
                        Console.WriteLine("Decoded JWT Claims:");
                        foreach (var claim in jwtToken.Claims)
                        {
                            Console.WriteLine($"{claim.Type}: {claim.Value}");
                        }

                        var claims = new List<Claim>();

                        var nomeCompleto = jwtToken.Claims.FirstOrDefault(c => c.Type == "nomeCompleto")?.Value;
                        var email = jwtToken.Claims.FirstOrDefault(c =>
                            c.Type == "email" || c.Type == ClaimTypes.Name ||
                            c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

                        if (!string.IsNullOrWhiteSpace(nomeCompleto))
                            claims.Add(new Claim(ClaimTypes.Name, nomeCompleto));
                        else if (!string.IsNullOrWhiteSpace(email))
                            claims.Add(new Claim(ClaimTypes.Name, email));

                        var role = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
                        if (!string.IsNullOrWhiteSpace(role))
                            claims.Add(new Claim(ClaimTypes.Role, role));

                        identity = new ClaimsIdentity(claims, "jwt");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao decodificar JWT: {ex.Message}");
                await _localStorage.RemoveItemAsync("authToken");
            }
        }

        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }


    public void NotifyUserAuthentication(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var claims = new List<Claim>();

        var nomeCompleto = jwtToken.Claims.FirstOrDefault(c => c.Type == "nomeCompleto")?.Value;
        var email = jwtToken.Claims.FirstOrDefault(c =>
            c.Type == "email" || c.Type == ClaimTypes.Name ||
            c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;

        if (!string.IsNullOrWhiteSpace(nomeCompleto))
        {
            claims.Add(new Claim(ClaimTypes.Name, nomeCompleto));
        }
        else if (!string.IsNullOrWhiteSpace(email))
        {
            claims.Add(new Claim(ClaimTypes.Name, email));
        }

        var role = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
        if (!string.IsNullOrWhiteSpace(role))
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void NotifyUserLogout()
    {
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }
}
