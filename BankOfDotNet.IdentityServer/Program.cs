using BankOfDotNet.IdentityServer.Data;
using BankOfDotNet.IdentityServer.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly.GetName().Name;
var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(dbConnectionString);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    // options.SignIn.RequireConfirmedEmail = true;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddIdentityServer()
    .AddTestUsers(Config.GetUsers())
    //.AddAspNetIdentity<IdentityUser>()    
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(
            dbConnectionString,
            opt => opt.MigrationsAssembly(assembly)
        );
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(
            dbConnectionString,
            opt => opt.MigrationsAssembly(assembly)
        );
    }).AddDeveloperSigningCredential();

builder.Services.AddControllers();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

// app.MapControllers(); // Replaced by this !!!
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// app.EnsureSeedData();

app.Run();