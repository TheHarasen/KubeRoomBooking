using AuthApplication.Abstracts;
using AuthApplication.Options;
using AuthDomain.Entities;
using AuthDomain.Requests;
using AuthInfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthApi;

public static class SeedData
{
	public static async void SeedAuth(this IServiceCollection services)
	{
		using var scope = services.BuildServiceProvider().CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
		var service = scope.ServiceProvider.GetRequiredService<IAccountService>();
		var options = scope.ServiceProvider.GetRequiredService<IOptions<AdminOptions>>().Value;

		if (context.Database.GetPendingMigrations().Any())
		{
			context.Database.Migrate();
		}

		bool noadmin = context.Users.FirstOrDefault(x => x.Email == options.Email) == null;

		if (noadmin)
		{
			await service.RegisterAsync(
				new RegisterRequest()
				{
					FirstName = options.FirstName,
					LastName = options.LastName,
					Email = options.Email,
					Password = options.Password
				}
				, Role.Admin);
		}
	}
}
