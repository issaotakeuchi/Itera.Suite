using Itera.Suite.Front; // Ajuste se precisar para seu namespace real
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace Front
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Diz onde seu App Razor ser� injetado no index.html
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // HttpClient configurado para apontar para SUA API REAL
            builder.Services.AddScoped(sp =>
                new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });
            // Troque pelo endere�o do seu backend quando estiver no Render

            // Ativa todos os servi�os do MudBlazor
            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}
