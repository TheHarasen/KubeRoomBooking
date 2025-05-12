using AuthApi.Handlers;
using AuthApplication.Abstracts;
using AuthApplication.Options;
using AuthApplication.Services;
using AuthDomain.Entities;
using AuthInfrastructure;
using AuthInfrastructure.Processors;
using AuthInfrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SubApi;
using System.Text;

namespace AuthApi;

public class Program
{
	public async static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		{
			builder.Services.Configure<AdminOptions>(
				builder.Configuration.GetSection(AdminOptions.AdminOptionsKey));

			builder.Services.AddIdentity<User, IdentityRole<Guid>>(opt =>
			{
				opt.Password.RequireDigit = true;
				opt.Password.RequireLowercase = true;
				opt.Password.RequireNonAlphanumeric = true;
				opt.Password.RequireUppercase = true;
				opt.Password.RequiredLength = 8;
				opt.User.RequireUniqueEmail = true;
			})
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddRoles<IdentityRole<Guid>>();

			builder.Services.AddDbContext<ApplicationDbContext>(opt =>
			{
				opt.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});

			builder.Services.AddScoped<IAuthTokenProcessor, AuthTokenProcessor>();
			builder.Services.AddScoped<IUserRepository, UserRepository>();
			builder.Services.AddScoped<IAccountService, AccountService>();
			builder.Services.AddScoped<IUserService, UserService>();

			builder.Services.AddAuthorization();
			builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
			builder.Services.AddHttpContextAccessor();

			builder.SetupBuilder();

			//remove if adding new migration
			await builder.Services.SeedAuth();
			builder.Services.AddControllers();
		}

		var app = builder.Build();
		{
			app.UseCors("localreactpolicy");
			app.UseExceptionHandler(_ => { });
			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllers();
			app.Run();
		}
	}
}
