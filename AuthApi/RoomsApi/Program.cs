using Microsoft.EntityFrameworkCore;
using RoomsApplication.Rooms;
using RoomsInfrastructure.EF;
using RoomsInfrastructure.Repositories;
using SubApi;

namespace RoomsApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			{
				builder.Services.AddDbContext<RoomsDbContext>(opt =>
				{
					opt.UseSqlServer(builder.Configuration["ConnectionStrings:RoomsConnection"]);
				});
				builder.Services.AddAuthorization();
				builder.SetupBuilder();
				builder.Services.AddScoped<IRoomRepository, EfRoomRepository>();
				builder.Services.AddScoped<IRoomService, RoomService>();
				builder.Services.AddControllers();
				builder.Services.SeedRooms();
			}

			var app = builder.Build();
			{
				app.UseAuthorization();
				app.MapControllers();
				app.Run();
			}

		}
	}
}
