using Flisekompaniet.PinCodeLock.Domain;
using Flisekompaniet.PinCodeLock.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddSingleton<IPinCodeRepository, FakePinCodeRepository>(); 
builder.Services.AddPinCode();


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