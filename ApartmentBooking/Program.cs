using ApartmentBooking.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<ApartmentBooking.App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://apartmentbooking-production.up.railway.app/")
});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ApiService>();

var host = builder.Build();

// Инициализация авторизации при старте
var auth = host.Services.GetRequiredService<AuthService>();
await auth.InitAsync();

await host.RunAsync();