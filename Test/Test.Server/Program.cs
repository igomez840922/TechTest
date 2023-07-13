using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Test.Data.ContextSoaint;
using Test.ServiceDependencies.Extensiones;
using Test.ServiceDependencies.Mappers;
using Test.Shared.Utilidades;

var builder = WebApplication.CreateBuilder(args);

// Configuracion del arranque del Serilog ====================================================
builder.Services.ConfiguracionLogger();
// ============================================================

// Configuracion de los cors =====================================================================
builder.Services.ConfiguracionCors();
// ===============================================================================================

// Configuracion de Token Bearer =================================================================
builder.Services.TokenSetup(builder.Configuration);

// Configuracion de AutoMapper ===================================================================
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddDbContext<SoaintContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SoaintDB")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Personalizacion de Swagger ===================
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Soaint Service",
        Version = "v1.0",
        Description = "Web Api .net 6",
        Contact = new OpenApiContact
        {
            Email = "fuentesmendes@gmail.com",
            Name = "David Alejandro Mendez Fuentes",
            Url = new Uri("https://github.com/DavidMendezF")
        }
    });
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor, ingresa el token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
// =============================================

// Inicializacion de parametros del appsettings.json =============================================================
builder.Services.Configure<MainSettings>(builder.Configuration.GetSection(nameof(MainSettings)));
builder.Services.AddSingleton<IMainSettings>(opcion => opcion.GetRequiredService<IOptions<MainSettings>>().Value);
// ===============================================================================================================

// Inyeccion de dependencias =========================================
builder.Services.InyeccionDependencias();
// =======================================================


var app = builder.Build();

// Llamado al uso de los cors
app.UseCors("AllowEverything");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Soaint Service - v1.0 - " + app.Environment.EnvironmentName));
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Soaint Service - v1.0 - " + app.Environment.EnvironmentName));
}

app.UseHttpsRedirection();
app.UseAuthentication(); // Se agrega para pa authenticacion por Token
app.UseAuthorization();

app.MapControllers();

app.Run();
