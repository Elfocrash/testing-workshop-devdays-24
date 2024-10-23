using Customers.Api.Repositories;
using Customers.Api.Services;
using Customers.WebApp.Database;
using Customers.WebApp.Repositories;
using Customers.WebApp.Services;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});
var config = builder.Configuration;
config.AddEnvironmentVariables("CustomersWebApp_");

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
    new NpgsqlConnectionFactory(config["Database:ConnectionString"]!));
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<ICustomerService, CustomerService>();
builder.Services.AddSingleton<IGitHubService, GitHubService>();

builder.Services.AddHttpClient("GitHub", httpClient =>
{
    httpClient.BaseAddress = new Uri(config["GitHub:ApiBaseUrl"]!);
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/vnd.github.v3+json");
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.UserAgent, $"Workshop-{Environment.MachineName}");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
await databaseInitializer.InitializeAsync();

app.Run();
