using AuthDomain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthInfrastructure;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}
	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<User>()
			.Property(u => u.FirstName).HasMaxLength(256);

		builder.Entity<User>()
			.Property(u => u.LastName).HasMaxLength(256);

		builder.Entity<User>()
		.Property(u => u.Role).HasConversion<string>();
	}
}
