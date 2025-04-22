using AuthApplication.Abstracts;
using AuthDomain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SubApplication.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthInfrastructure.Processors;

public class AuthTokenProcessor : IAuthTokenProcessor
{
	private readonly JwtOptions _options;
	private readonly IHttpContextAccessor _http;
	public AuthTokenProcessor(IOptions<JwtOptions> options, IHttpContextAccessor http)
	{
		_options = options.Value;
		_http = http;
	}

	public (string jwtToken, DateTime expiresAtUtc) GenerateJwtToken(User user)
	{
		var signingkey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(_options.Secret));

		var credentials = new SigningCredentials(
			signingkey,
			SecurityAlgorithms.HmacSha256);

		var claims = new[]
		{
			new Claim(ClaimTypes.Role, user.Role.ToString()),
			new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new Claim(JwtRegisteredClaimNames.Email, user.Email),
			new Claim(ClaimTypes.NameIdentifier, user.ToString()),
		};

		var expires = DateTime.UtcNow.AddMinutes(_options.ExpirationTimeInMinutes);

		var token = new JwtSecurityToken(
			issuer: _options.Issuer,
			audience: _options.Audience,
			claims: claims,
			expires: expires,
			signingCredentials: credentials);

		var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

		return (jwtToken, expires);
	}

	public string GenerateRefreshToken()
	{
		var randomNumber = new byte[64];
		using var rng = RandomNumberGenerator.Create();
		rng.GetBytes(randomNumber);
		return Convert.ToBase64String(randomNumber);
	}

	public void WriteAuthTokenAsHttpOnlyCookie(string cookieName, string token, DateTime expiration)
	{
		_http.HttpContext.Response.Cookies.Append(
			cookieName,
			token,
			new CookieOptions
			{
				HttpOnly = true,
				Expires = expiration,
				IsEssential = true,
				Secure = true,
				SameSite = SameSiteMode.None
			});
	}

	public void RemoveCookie(string cookieName)
	{
		_http.HttpContext.Response.Cookies.Delete(cookieName);
	}
}
