using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductsManagementSystem.Data;
using ProductsManagementSystem.Models;
using ProductsManagementSystem.ServiceContracts;
using ProductsManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Application Users
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>() //User Data
    .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>(); //Role Data

builder.Services.AddAuthorization();


// DI Service
builder.Services.AddScoped<IPartyService, PartyService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRateService, ProductRateService>();
builder.Services.AddScoped<IProductAssignmentService, ProductAssignmentService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

builder.Services.AddAuthorization();

builder.Services.AddAuthorization(option =>
{
    option.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); 
    option.AddPolicy("NotAuthorized", policy =>
    {
        policy.RequireAssertion(context =>
        {
            return !context.User.Identity.IsAuthenticated;
        });
    });
});

builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/Account/Login";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
