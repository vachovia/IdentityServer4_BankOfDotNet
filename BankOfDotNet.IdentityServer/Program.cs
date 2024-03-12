using BankOfDotNet.IdentityServer.ServerConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddMvc() // both are added
builder.Services.AddControllersWithViews();

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryIdentityResources(Config.GetIdentityResources()) // for implicit flow
    .AddInMemoryApiScopes(Config.GetApiScopes())
    .AddInMemoryApiResources(Config.GetAllApiResources())
    .AddInMemoryClients(Config.GetClients())
    .AddTestUsers(Config.GetUsers());

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

// Added
app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

// app.MapControllers(); // Replaced by this
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
