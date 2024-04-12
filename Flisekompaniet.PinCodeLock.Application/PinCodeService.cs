using Microsoft.AspNetCore.Http;
using SiteName.PinCodeLock.Domain;

namespace SiteName.PinCodeLock.Application;
public interface IPinCodeService
{
    bool IsValid(string kode);

    bool SetPinCodeIfValid(string kode, HttpContext httpContext);
}
internal class PinCodeService : IPinCodeService
{
    private readonly IPinCodeRepository _pinCodeRepository;

    public PinCodeService(IPinCodeRepository pinCodeRepository)
    {
        _pinCodeRepository = pinCodeRepository;
    }

    public bool IsValid(string kode)
    {
        var pinCode = _pinCodeRepository.GetPinCodeLock();
        return pinCode.IsUnlocked(kode);
    }

    public bool SetPinCodeIfValid(string kode, HttpContext httpContext)
    {
        if (IsValid(kode))
        {
            httpContext.Response.Cookies.Append(PinCodeMiddleware.PinCodeHeader, kode, new CookieOptions() { HttpOnly = true, SameSite = SameSiteMode.Strict, MaxAge = TimeSpan.FromDays(1) });
            return true;
        }

        return false;
    }
}
