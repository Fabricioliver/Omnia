using Ambev.DeveloperEvaluation.WebApi.Services;
using Ambev.DeveloperEvaluation.IoC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace Ambev.DeveloperEvaluation.WebApi;

public class WebApiLocalModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<SaleApplicationService>();
    }
}
