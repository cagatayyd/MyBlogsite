using Microsoft.EntityFrameworkCore;
using MyBlogsite.DbContexts;
using MyBlogsite.Entities;
// using MyBlogsite.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogContext>(
    dbContextOptions => dbContextOptions.UseSqlServer(
        "Server=DESKTOP-9F7C59U\\MSSQLSERV;Database=MyBlogDb;Trusted_Connection=True;TrustServerCertificate=true;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//builder.Services.AddScoped<INewsManageRepository, NewsManageRepository>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
