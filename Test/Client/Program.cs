using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using Test.Client;
using Test.Client.AuxLogin;
using Test.Client.ServiceRepository;
using OfficeOpenXml;

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

ConfigureServices(builder.Services);

await builder.Build().RunAsync();

void ConfigureServices(IServiceCollection services)
{

    services.AddSweetAlert2();
    services.AddScoped<IServiceRepository, ServiceRepository>();
    services.AddAuthorizationCore();
    services.AddScoped<AuthProviderJWT>();
    services.AddScoped<AuthenticationStateProvider, AuthProviderJWT>(proveedor =>
        proveedor.GetRequiredService<AuthProviderJWT>());

    services.AddScoped<ILoginservice, AuthProviderJWT>(proveedor =>
        proveedor.GetRequiredService<AuthProviderJWT>());


    services.AddScoped<RebuildToken>();
}