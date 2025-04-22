using AuthApplication.Abstracts;
using AuthDomain.Entities;
using AuthDomain.Exceptions.Auth;
using AuthDomain.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SubApplication.Options;

namespace AuthApplication.Services;

public class AccountService : IAccountService
{
	private readonly IAuthTokenProcessor _authTokenProcessor;
	private readonly UserManager<User> _userManager;
	private readonly IUserRepository _userRepository;
	private readonly JwtOptions _jwtOptions;

	public AccountService(IAuthTokenProcessor authTokenProcessor, UserManager<User> userManager, IUserRepository userRepository, IOptions<JwtOptions> jwtOptions)
	{
		_authTokenProcessor = authTokenProcessor;
		_userManager = userManager;
		_userRepository = userRepository;
		_jwtOptions = jwtOptions.Value;
	}

	public async Task RegisterAsync(RegisterRequest request, Role role)
	{
		bool userExists = await _userManager.FindByEmailAsync(request.Email) != null;

		if (userExists)
			throw new UserAlreadyExistsException(request.Email);

		User user = User.Create(request.Email, request.FirstName, request.LastName);
		user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);
		user.Role = role;

		var result = await _userManager.CreateAsync(user);

		if (!result.Succeeded)
			throw new RegistrationFailedException(result.Errors.Select(x => x.Description));
	}

	public async Task<(string, User)> LoginAsync(LoginRequest request)
	{
		var user = await _userManager.FindByEmailAsync(request.Email);

		if (user == null)
			throw new LoginFailedException(request.Email);

		if (!await _userManager.CheckPasswordAsync(user, request.Password))
			throw new LoginFailedException(request.Email);

		return await Generate(user);
	}

	public async Task<(string, User)> RefreshTokenAsync(string? refreshToken)
	{
		if (string.IsNullOrEmpty(refreshToken))
			throw new RefreshTokenException("Refresh token is missing");

		var user = await _userRepository.GetUserByRefreshTokenAsync(refreshToken);

		if (user == null)
			throw new RefreshTokenException("Unable to retrieve user for refresh token");

		if (user.RefreshTokenExpiresAtUtc < DateTime.UtcNow)
			throw new RefreshTokenException("Refresh token is expired");

		return await Generate(user);
	}

	private async Task<(string Token, User User)> Generate(User user)
	{
		var (jwtToken, expirationDateInUtc) = _authTokenProcessor.GenerateJwtToken(user);

		var refreshTokenValue = _authTokenProcessor.GenerateRefreshToken();

		//should be json setting
		var expiration = DateTime.UtcNow.AddDays(_jwtOptions.RefresherExpirationTimeInDays);

		user.RefreshToken = refreshTokenValue;
		user.RefreshTokenExpiresAtUtc = expiration;

		if ((await _userManager.UpdateAsync(user)).Succeeded)
			_authTokenProcessor.WriteAuthTokenAsHttpOnlyCookie("REFRESH_TOKEN", user.RefreshToken, expiration);
		return (jwtToken, user);
	}

	public async Task LogOutAsync(string? token)
	{
		if (string.IsNullOrEmpty(token))
			return;

		_authTokenProcessor.RemoveCookie("REFRESH_TOKEN");

		var user = await _userRepository.GetUserByRefreshTokenAsync(token);
		if (user != null)
		{
			user.RefreshToken = null;
			user.RefreshTokenExpiresAtUtc = null;
			await _userManager.UpdateAsync(user);
		}

	}
}
