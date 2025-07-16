using System.Net.Http.Json;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Itera.Suite.Front.Shared.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Itera.Suite.Front.Services.Auth;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly CustomAuthStateProvider _authStateProvider;

    public AuthService(HttpClient httpClient, 
                       ILocalStorageService localStorage, 
                       AuthenticationStateProvider authStateProvider)
    {
        _httpClient = httpClient;
        _localStorage = localStorage;
        _authStateProvider = (CustomAuthStateProvider)authStateProvider;
    }

    /// <summary>
    /// Faz login com email/senha, salva JWT, notifica o auth provider
    /// </summary>
    public async Task<(bool IsSuccess, string ErrorMessage)> Login(LoginRequest loginModel)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/Auth/login", loginModel);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                var token = result?.Token;

                if (string.IsNullOrWhiteSpace(token))
                    return (false, "Token não retornado pela API.");

                // ✅ Salva token no LocalStorage
                await _localStorage.SetItemAsync("authToken", token);

                // ✅ Notifica o AuthStateProvider
                _authStateProvider.NotifyUserAuthentication(token);

                // ✅ (Opcional) Se usar HttpClient direto, define o header:
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                return (true, null);
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return (false, $"Erro ao autenticar: {error}");
            }
        }
        catch (Exception ex)
        {
            return (false, $"Erro de rede: {ex.Message}");
        }
    }

    /// <summary>
    /// Faz logout, limpa JWT, notifica o auth provider
    /// </summary>
    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        _authStateProvider.NotifyUserLogout();

        // ✅ Remove header Bearer se usar HttpClient direto
        _httpClient.DefaultRequestHeaders.Authorization = null;
    }
}

public class LoginRequest
{
    [Required]
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; }
}

public class LoginResponse
{
    public string Token { get; set; }
}
