using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SkaftoBageriWMS.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json
var configuration = builder.Configuration;

// Add services to the container
builder.Services.AddControllersWithViews();

// Configure MySQL connection and register AppDbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection"))
    )
);

// Enable session with options
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout setting
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Ensure the session cookie is essential
});

// Add authentication middleware
builder.Services.AddAuthentication("YourCookieAuthScheme")
    .AddCookie("YourCookieAuthScheme", options =>
    {
        options.LoginPath = "/Account/Login"; // Redirect path for login
        options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect path for access denied
    });

// Use Dependency Injection for IHttpContextAccessor (required for session and user context)
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Build the app
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Detailed error page for development
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Custom error handling page
    app.UseHsts(); // Use HTTP Strict Transport Security
}

app.UseHttpsRedirection(); // Redirect HTTP to HTTPS
app.UseStaticFiles(); // Serve static files (CSS, JS, images)

// Middleware pipeline
app.UseRouting(); // Enable routing

app.UseSession(); // Use session before authentication
app.UseAuthentication(); // Use authentication middleware
app.UseAuthorization(); // Use authorization middleware

// Define the default route pattern
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map the Account controller's Dashboard action
app.MapControllerRoute(
    name: "dashboard",
    pattern: "Dashboard",
    defaults: new { controller = "Account", action = "Dashboard" });

// Run the application
app.Run();
