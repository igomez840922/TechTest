using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Test.Client;
using Test.Client.Servicios;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7180/") });

builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// MudBlazor
builder.Services.AddMudServices();
// BlazoredStorage
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
