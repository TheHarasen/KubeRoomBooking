using AuthDomain.Entities;
using AuthDomain.Exceptions;
using AuthDomain.Requests;
using Microsoft.AspNetCore.Identity;

namespace AuthApplication.Abstracts;

public interface IAccountService
{
	Task RegisterAsync(RegisterRequest request, Role role);

	Task<(string accessToken, User user)> LoginAsync(LoginRequest request);

	Task<(string accessToken, User user)> RefreshTokenAsync(string? refreshToken);

	Task LogOutAsync(string? token);
}
