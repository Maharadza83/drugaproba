namespace drugaproba.Models;

public class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class LoginResponse
{
    public required string Token { get; set; }
    public string? FullName { get; set; }
}

public class RegisterRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? FullName { get; set; }
    public string? OrganizationName { get; set; }
    public string? ProjectName { get; set; }
}
