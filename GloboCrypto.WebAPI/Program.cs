using GloboCrypto.WebAPI;
using GloboCrypto.WebAPI.Models;
using GloboCrypto.WebAPI.Services.Coins;
using GloboCrypto.WebAPI.Services.Data;
using GloboCrypto.WebAPI.Services.Events;
using GloboCrypto.WebAPI.Services.Http;
using GloboCrypto.WebAPI.Services.Notifications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "Open",
        builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var configuration = builder.Configuration;
var apiKey = configuration["APIKey"];
var secret = configuration["Secret"];

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpService, HttpService>();
builder.Services.AddSingleton<ILocalDbService>(new LocalDbService("CoinAPIData.db"));
builder.Services.AddSingleton<IEventService, EventService>();
builder.Services.AddSingleton<ICoinService, CoinService>();
builder.Services.AddSingleton<INotificationService, NotificationService>();

builder.Services.AddTransient<IAuthorizationHandler, ApiKeyRequirementHandler>();
builder.Services.AddAuthorization(authConfig =>
{
    authConfig.AddPolicy("ApiKeyPolicy",
        policyBuilder => policyBuilder
        .AddRequirements(new ApiKeyRequirement(new[] { apiKey })));
});
var key = Encoding.ASCII.GetBytes(secret);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("Open");

app.Run();
