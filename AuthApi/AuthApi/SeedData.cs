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
	public static async Task SeedAuth(this IServiceCollection services)
	{
		using var scope = services.BuildServiceProvider().CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
		var service = scope.ServiceProvider.GetRequiredService<IAccountService>();
		var options = scope.ServiceProvider.GetRequiredService<IOptions<AdminOptions>>().Value;

		try
		{
			if (context.Database.GetPendingMigrations().Any())
				context.Database.Migrate();

			if (!context.Users.Any(x => x.Email == options.Email))
			{
				await service.RegisterAsync(new RegisterRequest
				{
					FirstName = options.FirstName,
					LastName = options.LastName,
					Email = options.Email,
					Password = options.Password
				}, Role.Admin);
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Seeding error: {ex.Message}");
		}

	}
}
