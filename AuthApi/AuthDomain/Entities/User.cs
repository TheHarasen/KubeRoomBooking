using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDomain.Entities;

public class User : IdentityUser<Guid>
{
	public string FirstName { get; set; } = null!;
	public string LastName { get; set; } = null!;
	public Role Role { get; set; } = Role.Student;
	public string? RefreshToken { get; set; }
	public DateTime? RefreshTokenExpiresAtUtc { get; set; }

	public static User Create(string email, string firstName, string lastName)
	{
		return new User
		{
			Email = email,
			UserName = email,
			FirstName = firstName,
			LastName = lastName
		};
	}

	public void Copy(User user)
	{
		FirstName = user.FirstName;
		LastName = user.LastName;
		Role = user.Role;
		RefreshToken = user.RefreshToken;
		RefreshTokenExpiresAtUtc = user.RefreshTokenExpiresAtUtc;
	}

	public override string ToString()
	{
		return FirstName + " " + LastName;
	}
}
public enum Role
{
	Student,
	Teacher,
	Admin
}