using Investimento.Cliente.Custodia.Domain.Interfaces.Repositories;
using Investimento.Cliente.Custodia.Domain.Interfaces.Services;
using Investimento.Cliente.Custodia.Domain.Services;
using Investimento.Cliente.Custodia.Infrastructure.Repositories;
using Investimento.Cliente.Custodia.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Investimento.Cliente.Custodia.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ITesouroDiretoService, TesouroDiretoService>();
            services.AddScoped<IRendaFixaService, RendaFixaService>();
            services.AddScoped<IFundoService, FundoService>();
            services.AddScoped<ICacheService, RedisService>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient("investimento", options =>
            {
                options.DefaultRequestHeaders.Add("Accept", "application/json");
            })
            .AddPolicyHandler(PollyConfiguration.GetRetryPolicy())
            .AddPolicyHandler(PollyConfiguration.GetFallback())
            .AddPolicyHandler(PollyConfiguration.GetCircuitBreakerPolicy());

            return services;
        }
    }
}
