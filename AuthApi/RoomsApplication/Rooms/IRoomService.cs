using RoomsDomain.Entities;

namespace RoomsApplication.Rooms;

public interface IRoomService
{
	Task Delete(Guid id);
	Task<Room?> Get(Guid id);
	Task<IEnumerable<Room>> GetAll();
	Task Update(Room room);
	Task Create(Room room);
}
