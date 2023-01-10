using Core.Domain;
using Data;
using Microsoft.EntityFrameworkCore;
using UI.ServiceExtesions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
var cfg = builder.Configuration;
builder.Services
    .InstallBusinessServices(cfg)
    .InstallDataServices(cfg);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ComixLibContext>();
    await context.Database.EnsureCreatedAsync();
    if (!context.Admins.Any())
    {
        await context.Admins.AddAsync(new Admin { Login = "Login", Password = "123" });
    }
    await context.SaveChangesAsync();
    await context.Database.EnsureCreatedAsync();
}

app.Run();
