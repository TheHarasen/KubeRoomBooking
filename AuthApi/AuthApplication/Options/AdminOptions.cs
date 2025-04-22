using AuthDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApplication.Options;

public class AdminOptions
{
	public const string AdminOptionsKey = "AdminOptions";

	public string AdminRegisterPassword { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public Role Role { get; set; } = Role.Admin;
}
