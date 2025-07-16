using System.Net.Http.Json;

namespace Itera.Suite.Front.Services.Fornecedor;

public class FornecedorService
{
    private readonly HttpClient _http;

    public FornecedorService(HttpClient http)
    {
        _http = http;
    }

    public async Task<IEnumerable<FornecedorModel>> GetAll()
        => await _http.GetFromJsonAsync<IEnumerable<FornecedorModel>>("api/fornecedores");

    public async Task<FornecedorModel> GetById(Guid id)
        => await _http.GetFromJsonAsync<FornecedorModel>($"api/fornecedores/{id}");

    public async Task Add(FornecedorModel fornecedor)
        => await _http.PostAsJsonAsync("api/fornecedores", fornecedor);

    public async Task Update(FornecedorModel fornecedor)
        => await _http.PutAsJsonAsync($"api/fornecedores/{fornecedor.Id}", fornecedor);

    public async Task Delete(Guid id)
        => await _http.DeleteAsync($"api/fornecedores/{id}");
}
