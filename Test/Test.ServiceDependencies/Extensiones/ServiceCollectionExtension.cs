using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using Test.ServiceDependencies.Context;

namespace Test.ServiceDependencies.Extensiones
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Configuracion de los Logs, en este caso del Serilog
        /// </summary>
        /// <param name="services"></param>
        public static void ConfiguracionLogger(this IServiceCollection services)
        {
            var path = AppContext.BaseDirectory + (@"\Logs\eventos.txt");
            Log.Logger = new LoggerConfiguration()
                             .MinimumLevel.Verbose()
                             .WriteTo.File(path)
                             .CreateLogger();
        }

        /// <summary>
        /// Inyeccion de dependencias de los servicios
        /// </summary>
        /// <param name="services"></param>
        public static void InyeccionDependencias(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioContext, UsuarioContext>();
            services.AddScoped<IProductoContext, ProductoContext>();
        }

        /// <summary>
        /// Configuracion de los Cors del api
        /// </summary>
        /// <param name="services"></param>
        public static void ConfiguracionCors(this IServiceCollection services)
        {
            services.AddCors(opcion => opcion.AddPolicy("AllowEverything", builder => builder.AllowAnyOrigin()
                                                                                         .AllowAnyMethod()
                                                                                         .AllowAnyHeader()));
        }

        /// <summary>
        /// Configuracion del la parametrizacion del token
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void TokenSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opcion =>
            {
                opcion.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["MainSettings:Domain"],
                    ValidAudience = configuration["MainSettings:Domain"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["MainSettings:KeyToken"]))
                };
            });
        }
    }
}
