using GloboCrypto.WebAPI.Services.Coins;
using GloboCrypto.WebAPI.Services.Data;
using GloboCrypto.WebAPI.Services.Events;
using GloboCrypto.WebAPI.Services.Http;
using GloboCrypto.WebAPI.Services.Notifications;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpService, HttpService>();
builder.Services.AddSingleton<ILocalDbService>(new LocalDbService("CoinAPIData.db"));
builder.Services.AddSingleton<IEventService, EventService>();
builder.Services.AddSingleton<ICoinService, CoinService>();
builder.Services.AddSingleton<INotificationService, NotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
