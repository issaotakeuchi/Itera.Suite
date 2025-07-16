using System.Net.Http.Json;

namespace Itera.Suite.Front.Services.Cliente;

public class ClienteService
{
    private readonly HttpClient _http;

    public ClienteService(HttpClient http)
    {
        _http = http;
    }

    public async Task<IEnumerable<ClienteModel>> GetAll()
        => await _http.GetFromJsonAsync<IEnumerable<ClienteModel>>("api/Clientes");

    public async Task<ClienteModel> GetById(Guid id)
        => await _http.GetFromJsonAsync<ClienteModel>($"api/Clientes/{id}");

    public async Task Add(ClienteModel cliente)
        => await _http.PostAsJsonAsync("api/Clientes", cliente);

    public async Task Update(ClienteModel cliente)
        => await _http.PutAsJsonAsync($"api/Clientes/{cliente.Id}", cliente);

    public async Task Delete(Guid id)
        => await _http.DeleteAsync($"api/Clientes/{id}");
}
