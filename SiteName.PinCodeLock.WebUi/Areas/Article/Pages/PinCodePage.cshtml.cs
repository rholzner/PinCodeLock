using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiteName.PinCodeLock.Application;

namespace Article.Presentation.MyFeature.Pages;

public class PinCodePage : PageModel
{
    private readonly IPinCodeService _pinCodeService;

    public PinCodePage(IPinCodeService pinCodeService)
    {
        _pinCodeService = pinCodeService;
    }

    [BindProperty]
    public string PinCode { get; set; }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (_pinCodeService.SetPinCodeIfValid(PinCode, this.HttpContext))
        {
            var returnUrl = Request.Query["returnUrl"];

            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/";
            }

            var restOfQueries = string.Join('&', Request.Query.Where(x => x.Key != "returnUrl").Select(x => $"{x.Key}={x.Value}"));
            if (!string.IsNullOrEmpty(restOfQueries))
            {
                returnUrl = returnUrl + $"?{restOfQueries}";
            }
            return Redirect(returnUrl);
        }

        return Page();
    }
}
