using ForeignExchange.Api.Database;
using ForeignExchange.Api.Repositories;
using ForeignExchange.Api.Services;
using ForeignExchange.Api.Validation;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
    new NpgsqlConnectionFactory(config["Database:ConnectionString"]!));
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddSingleton<IRatesRepository, RatesRepository>();
builder.Services.AddSingleton<IQuoteService, QuoteService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<ValidationExceptionMiddleware>();

app.MapControllers();

var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
await databaseInitializer.InitializeAsync();

app.Run();
