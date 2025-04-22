using Microsoft.EntityFrameworkCore;
using RoomsDomain.Entities;

namespace RoomsInfrastructure.EF;

public class RoomsDbContext : DbContext
{
	public RoomsDbContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<Room> Rooms { get; set; } = null!;
	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<Room>().HasKey(r => r.Id);

		builder.Entity<Room>().Property(r => r.Id).ValueGeneratedNever();

		builder.Entity<Room>().Property(r => r.M2);
	}
}
