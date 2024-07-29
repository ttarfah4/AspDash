using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using Warehouse.DB.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var WarehouseConnectionString = builder.Configuration.GetConnectionString("WarehouseContext") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<WarehouseDbContext>(o =>
{
    o.UseSqlServer(WarehouseConnectionString);
});

builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetService<WarehouseDbContext>();
        context.Database.Migrate();
    }
    catch (Exception)
    {
        throw;
    }
}
app.UseSession();
app.Run();
