namespace AuthApi.DTOs;

public record LoginResponse(string AccessToken, Guid Id, string FirstName, string LastName, string Email);