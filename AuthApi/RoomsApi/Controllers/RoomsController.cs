using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomsApi.DTOs;
using RoomsApplication.Rooms;
using RoomsDomain.Entities;

namespace RoomsApi.Controllers;
[ApiController]
[Authorize(Roles = "Admin")]
[Route("rooms")]
public class RoomsController : ControllerBase
{
	private readonly IRoomService _roomService;

	public RoomsController(IRoomService roomService)
	{
		_roomService = roomService;
	}

	[HttpGet]
	public async Task<IResult> GetAll()
	{
		return Results.Ok(await _roomService.GetAll());
	}

	[HttpPost]
	public async Task<IResult> Register(CreateRoomRequest request)
	{
		await _roomService.Create(new Room() { M2 = request.M2});
		return Results.Ok();
	}
}
