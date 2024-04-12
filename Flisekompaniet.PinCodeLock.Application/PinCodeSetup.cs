using Flisekompaniet.PinCodeLock.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Flisekompaniet.PinCodeLock.Application;

public static class PinCodeSetup
{
    public static IServiceCollection AddPinCode(this IServiceCollection services)
    {
        services.AddSingleton<IPinCodeService, PinCodeService>();
        services.AddSingleton<PinCodeMiddleware>();

        return services;
    }

    public static IServiceCollection AddPinCode<PinCodeRepository>(this IServiceCollection services) where PinCodeRepository : class, IPinCodeRepository
    {
        services.AddSingleton<IPinCodeRepository, PinCodeRepository>();
        services.AddPinCode();
        return services;
    }

    public static IApplicationBuilder UsePinCode(this IApplicationBuilder app)
    {
        app.UseMiddleware<PinCodeMiddleware>();
        return app;
    }
}