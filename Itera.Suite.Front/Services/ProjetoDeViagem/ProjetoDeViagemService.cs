using Itera.Suite.Front.Services.ProjetoDeViagem;
using System.Net.Http.Json;

namespace Itera.Suite.Front.Services.ProjetoDeViagem;


public class ProjetoDeViagemService
{
    private readonly HttpClient _http;

    public ProjetoDeViagemService(HttpClient http)
    {
        _http = http;
    }

    public async Task<IEnumerable<ProjetoDeViagemModel>> GetAll()
    => await _http.GetFromJsonAsync<IEnumerable<ProjetoDeViagemModel>>("api/projetosdeviagem") ?? Enumerable.Empty<ProjetoDeViagemModel>();

    public async Task<bool> Add(ProjetoDeViagemCreateModel projetoDeViagem)
    {
        var response = await _http.PostAsJsonAsync("api/projetosdeviagem", projetoDeViagem);
        return response.IsSuccessStatusCode;
    }
}
