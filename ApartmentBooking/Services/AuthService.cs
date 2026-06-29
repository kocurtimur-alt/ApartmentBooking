using ApartmentBooking.Models;
using Blazored.LocalStorage;
using System.Net.Http.Json;

namespace ApartmentBooking.Services;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly ILocalStorageService _storage;

    public AuthService(HttpClient http, ILocalStorageService storage)
    {
        _http = http;
        _storage = storage;
    }

    public int? UserId { get; private set; }
    public string? UserName { get; private set; }
    public bool IsLoggedIn => UserId.HasValue;

    public event Action? OnAuthChanged;

    public async Task InitAsync()
    {
        var token = await _storage.GetItemAsStringAsync("token");
        if (!string.IsNullOrEmpty(token))
        {
            UserId = await _storage.GetItemAsync<int>("userId");
            UserName = await _storage.GetItemAsStringAsync("userName");
            _http.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<bool> RegisterAsync(string name, string email, string password)
    {
        var res = await _http.PostAsJsonAsync("api/auth/register",
            new { name, email, password });
        if (!res.IsSuccessStatusCode) return false;
        var data = await res.Content.ReadFromJsonAsync<AuthResponse>();
        await SaveAuth(data!);
        return true;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var res = await _http.PostAsJsonAsync("api/auth/login",
            new { email, password });
        if (!res.IsSuccessStatusCode) return false;
        var data = await res.Content.ReadFromJsonAsync<AuthResponse>();
        await SaveAuth(data!);
        return true;
    }

    public async Task LogoutAsync()
    {
        await _storage.RemoveItemAsync("token");
        await _storage.RemoveItemAsync("userId");
        await _storage.RemoveItemAsync("userName");
        UserId = null;
        UserName = null;
        _http.DefaultRequestHeaders.Authorization = null;
        OnAuthChanged?.Invoke();
    }

    private async Task SaveAuth(AuthResponse data)
    {
        await _storage.SetItemAsStringAsync("token", data.Token);
        await _storage.SetItemAsync("userId", data.UserId);
        await _storage.SetItemAsStringAsync("userName", data.Name);
        UserId = data.UserId;
        UserName = data.Name;
        _http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", data.Token);
        OnAuthChanged?.Invoke();
    }
}
