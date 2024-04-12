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
            return Redirect("/");
        }

        return Page();
    }
}
