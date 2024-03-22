using BankOfDotNet.API.Data;
using BankOfDotNet.API.Static;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<BankContext>(options =>
{
    options.UseSqlServer(connectionString);
    // options.UseInMemoryDatabase("BankingDb");
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, o =>
{
    o.Authority = "https://localhost:7000/";
    o.RequireHttpsMetadata = true;
    // o.ApiName = "bankOfDotNetApi";
    o.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = false
    };
}, o => { });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<CheckAuthorizeOperationFilter>();
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://localhost:7000/connect/authorize"),
                TokenUrl = new Uri("https://localhost:7000/connect/token"),
                Scopes = new Dictionary<string, string> {
                    { "bankOfDotNetApi", "Customer API for bank Of DotNet" }
                },                
            }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("http://localhost:5001/swagger/v1/swagger.json", "Bank Of DotNet API");
        options.OAuthClientId("swaggerapiui");
        options.OAuthAppName("Swagger API UI");
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
