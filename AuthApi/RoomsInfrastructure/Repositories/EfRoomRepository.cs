using Microsoft.EntityFrameworkCore;
using RoomsApplication.Rooms;
using RoomsDomain.Entities;
using RoomsInfrastructure.EF;

namespace RoomsInfrastructure.Repositories;

public class EfRoomRepository : IRoomRepository
{
	private readonly RoomsDbContext _context;

	public EfRoomRepository(RoomsDbContext context)
	{
		_context = context;
	}

	public async Task Create(Room room)
	{
		await _context.Rooms.AddAsync(room);
		await _context.SaveChangesAsync();
	}

	public async Task Delete(Guid id)
	{
		//avoid tracker, no need to savechanges
		await _context.Rooms.Where(r => r.Id == id).ExecuteDeleteAsync();
	}

	public async Task<Room?> Get(Guid id)
	{
		return await _context.FindAsync<Room>(id);
	}

	public async Task<IEnumerable<Room>> GetAll()
	{
		return await _context.Rooms.ToArrayAsync();
	}

	public async Task Update(Room room)
	{
		//avoid tracker, no need to savechanges
		await _context.Rooms.Where(r => r.Id == room.Id)
			.ExecuteUpdateAsync(setters => setters
			.SetProperty(r => r.M2, room.M2));
	}
}
