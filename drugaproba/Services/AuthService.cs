using System.Net.Http.Json;
using drugaproba.Models;

namespace drugaproba.Services;

public class AuthService
{
    private readonly HttpClient _http;
    public string? Token { get; private set; }

    public AuthService(HttpClient http) => _http = http;

    public async Task<bool> Login(LoginRequest req)
    {
        var res = await _http.PostAsJsonAsync("api/auth/login", req);
        if (!res.IsSuccessStatusCode) return false;
        var dto = await res.Content.ReadFromJsonAsync<LoginResponse>();
        Token = dto?.Token;
        return Token is not null;
    }

    public async Task<bool> Register(RegisterRequest req)
        => (await _http.PostAsJsonAsync("api/auth/register", req)).IsSuccessStatusCode;
}
