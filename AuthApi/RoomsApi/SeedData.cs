using Microsoft.EntityFrameworkCore;
using RoomsInfrastructure.EF;

namespace RoomsApi;

public static class SeedData
{
	public static void SeedRooms(this IServiceCollection services)
	{
		using var scope = services.BuildServiceProvider().CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<RoomsDbContext>();

		try
		{
			if (context.Database.GetPendingMigrations().Any())
			{
				context.Database.Migrate();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Seeding error: {ex.Message}");
		}
	}
}
