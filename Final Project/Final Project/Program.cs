using Domain.Models;
using Final_Project.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Repository;
using Serilog;
using Service;
using Service.Helpers;
using Service.Services;
using Service.Services.Interfaces;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//var conString = builder.Configuration.GetConnectionString("Default") ??
//     throw new InvalidOperationException("Connection string 'Default'" +
//    " not found.");

//builder.Services.AddDbContext<Db>(options =>
//    options.UseSqlServer(conString));

builder.Services.AddDbContext<Db>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailConfig"));



builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<Db>()
                                           .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{

    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;

});


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    // .WriteTo.Seq("http://localhost:5341") 
    .Enrich.FromLogContext()
    .CreateLogger();


builder.Services.AddHttpContextAccessor();
builder.Services.AddRepositoryLayer();
builder.Services.AddServiceLayer();

builder.Host.UseSerilog();


//builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
//builder.Services.AddScoped<IEmailService, EmailService>();
//builder.Services.AddScoped<IAccountService, AccountService>();




//builder.Services.AddRepositoryLayer();
//builder.Services.AddServiceLayer();




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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
