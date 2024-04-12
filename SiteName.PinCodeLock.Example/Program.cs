using SiteName.PinCodeLock.Application;
using SiteName.PinCodeLock.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

//INFO: add pin code service
builder.Services.AddPinCode<FakePinCodeRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

//INFO: add use pin code middleware
app.UsePinCode();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();




public class FakePinCodeRepository : IPinCodeRepository
{
    public PinCodeLock GetPinCodeLock()
    {
        return new PinCodeLock("1234", "4321");
    }
}