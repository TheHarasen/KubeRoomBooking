using AuthApplication.Abstracts;
using AuthDomain.Entities;
using AuthDomain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AuthApi.Controllers;
[ApiController]
[Route("admins")]
public class AdminsController : ControllerBase
{
	private readonly IUserService _userService;

	public AdminsController(IUserService userService)
	{
		_userService = userService;
	}

	[Authorize(Roles = "Admin")]
	[HttpGet]
	public async Task<IResult> GetAll()
	{
		return Results.Ok(await _userService.GetUsersAsync(Role.Admin));
	}

	[Route("/id/{id}")]
	[Authorize(Roles = "Admin")]
	[HttpGet]
	public async Task<IResult> GetById(Guid id)
	{
		var result = await _userService.GetUserByIdAsync(id, Role.Admin);
		return Results.Ok(result);
	}

	[Route("/email/{email}")]
	[Authorize(Roles = "Admin")]
	[HttpGet]
	public async Task<IResult> GetByEmail(string email)
	{
		var result = await _userService.GetUserByEmailAsync(email, Role.Admin);
		return Results.Ok(result);
	}
}
