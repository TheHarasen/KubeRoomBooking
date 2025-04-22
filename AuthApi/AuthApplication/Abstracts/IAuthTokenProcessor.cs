using AuthDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApplication.Abstracts;

public interface IAuthTokenProcessor
{
	(string jwtToken, DateTime expiresAtUtc) GenerateJwtToken(User user);
	string GenerateRefreshToken();
	void WriteAuthTokenAsHttpOnlyCookie(string cookieName, string token, DateTime expiration);
	void RemoveCookie(string cookie);
}
