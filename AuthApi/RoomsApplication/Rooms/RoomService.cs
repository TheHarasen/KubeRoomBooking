using RoomsDomain.Entities;

namespace RoomsApplication.Rooms;

public class RoomService : IRoomService
{
	private readonly IRoomRepository _roomRepository;

	public RoomService(IRoomRepository roomRepository)
	{
		_roomRepository = roomRepository;
	}

	public async Task Create(Room room)
	{
		room.Id = Guid.NewGuid();
		await _roomRepository.Create(room);
	}

	public async Task Delete(Guid id)
	{
		await _roomRepository.Delete(id);
	}

	public async Task<Room?> Get(Guid id)
	{
		return await _roomRepository.Get(id);
	}

	public async Task<IEnumerable<Room>> GetAll()
	{
		return await _roomRepository.GetAll();
	}

	public async Task Update(Room room)
	{
		await _roomRepository.Update(room);
	}
}
