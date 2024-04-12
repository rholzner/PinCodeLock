using Microsoft.AspNetCore.Http;

namespace SiteName.PinCodeLock.Application;

public class PinCodeMiddleware : IMiddleware
{
    private readonly IPinCodeService _pinCodeService;

    public PinCodeMiddleware(IPinCodeService pinCodeService)
    {
        _pinCodeService = pinCodeService;
    }

    public const string PinCodeHeader = "testsitePinCode";
    public static string PinCodePagePath = "/system/PinCode";

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if ((context.User?.Identity?.IsAuthenticated ?? false))
        {
            await next.Invoke(context);
            return;
        }

        if (context.Request.Path.Equals(PinCodePagePath, StringComparison.InvariantCultureIgnoreCase) ||
            context.Request.Path.StartsWithSegments("/api", StringComparison.InvariantCultureIgnoreCase) ||
            context.Request.Path.StartsWithSegments("/system", StringComparison.InvariantCultureIgnoreCase) ||
            context.Request.Path.StartsWithSegments("/health", StringComparison.InvariantCultureIgnoreCase))
        {
            await next.Invoke(context);
            return;
        }

        var hasPinCode = context.Request.Headers.TryGetValue(PinCodeHeader, out var pinCode);
        if (hasPinCode && _pinCodeService.IsValid(pinCode))
        {
            await next.Invoke(context);
            return;
        }

        var cookiehasPinCode = context.Request.Cookies.TryGetValue(PinCodeHeader, out string cookiePinCode);
        if (cookiehasPinCode && _pinCodeService.IsValid(cookiePinCode))
        {
            await next.Invoke(context);
            return;
        }


        var oldPath = context.Request.Path;
        string oldQueryString = context.Request.QueryString.Value;

        if (oldQueryString?.StartsWith("?") ?? false)
        {
            //INFO: remove the first character '?' and replace with '&'
            oldQueryString = $"&{oldQueryString.Substring(1)}";
        }

        var pathToRedirect = $"{PinCodePagePath}?returnUrl={oldPath}{oldQueryString}";

        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        context.Response.Redirect(pathToRedirect, false);
    }
}
