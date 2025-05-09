using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.EntityFrameworkCore;
using Projet_.NET_Core_M2_Devops_Mr_Naina;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Session
//builder.Services.AddDistributedMemoryCache(); // Required for storing session in memory (for development).
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set Session Timeout duration
    options.Cookie.HttpOnly = true; // Ensures the cookie is only accessible via HTTP requests
    options.Cookie.IsEssential = true; // Makes session cookie essential for the app to work
});

// DinktoPdf
builder.Services.AddSingleton<ITools, PdfTools>();
builder.Services.AddSingleton<IConverter, BasicConverter>();

builder.Services.AddControllersWithViews();

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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
