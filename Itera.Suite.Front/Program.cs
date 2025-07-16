using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using Itera.Suite.Front;
using Itera.Suite.Front.Services.Auth;
using Itera.Suite.Front.Services.Shared;
// Importa seus outros services de domínio também
/*using Itera.Suite.Front.Services.ProjetoDeViagem;
using Itera.Suite.Front.Services.ItemDeCusto;*/
using Itera.Suite.Front.Services.Fornecedor;
using Itera.Suite.Front.Services.Cliente;
using Itera.Suite.Front.Shared.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 👉 HttpClient Base
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:5146/")
});

// ✅ LocalStorage para guardar o token JWT
builder.Services.AddBlazoredLocalStorage();

// ✅ Autorização do Blazor WASM
builder.Services.AddAuthorizationCore();

// ✅ Seu CustomAuthStateProvider (rotas protegidas)
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

// Registra o DelegatingHandler
builder.Services.AddTransient<ApiHttpClientHandler>();

// HttpClient usando o handler
builder.Services.AddScoped(sp =>
{
    var localStorage = sp.GetRequiredService<ILocalStorageService>();

    var handler = new ApiHttpClientHandler(localStorage)
    {
        InnerHandler = new HttpClientHandler()
    };

    var client = new HttpClient(handler)
    {
        BaseAddress = new Uri("https://localhost:5146/")
    };

    return client;
});


// ✅ Serviços de Auth
builder.Services.AddScoped<AuthService>();

// ✅ Seus CRUD Services
/*builder.Services.AddScoped<ProjetoDeViagemService>();
builder.Services.AddScoped<ItemDeCustoService>();*/
builder.Services.AddScoped<FornecedorService>();
builder.Services.AddScoped<ClienteService>();


builder.Services.AddScoped<Radzen.NotificationService>();
builder.Services.AddScoped<Radzen.DialogService>();
builder.Services.AddScoped<Radzen.TooltipService>();
builder.Services.AddScoped<Radzen.ContextMenuService>();

await builder.Build().RunAsync();
