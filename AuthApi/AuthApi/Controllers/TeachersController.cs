using AuthApplication.Abstracts;
using AuthDomain.Entities;
using AuthDomain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AuthApi.Controllers;
[ApiController]
[Route("teachers")]
public class TeacherController : ControllerBase
{
	private readonly IAccountService _accountService;
	private readonly IUserService _userService;

	public TeacherController(IAccountService accountService, IUserService userService)
	{
		_accountService = accountService;
		_userService = userService;
	}

	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IResult> Register(RegisterRequest request)
	{
		await _accountService.RegisterAsync(request, Role.Teacher);
		return Results.Ok();
	}

	[Authorize(Roles = "Admin")]
	[HttpPut]
	public async Task<IResult> Edit(EditRequest request)
	{
		await _userService.EditUserAsync(request, Role.Teacher);
		return Results.Ok();
	}

	[Authorize(Roles = "Admin")]
	[HttpDelete]
	public async Task<IResult> Delete(Guid id)
	{
		await _userService.DeleteUserAsync(id, Role.Teacher);
		return Results.Ok();
	}

	[Authorize]
	[HttpGet]
	public async Task<IResult> GetAll()
	{
		return Results.Ok(await _userService.GetUsersAsync(Role.Teacher));
	}

	[Route("/id/{id}")]
	[Authorize]
	[HttpGet]
	public async Task<IResult> GetById(Guid id)
	{
		var result = await _userService.GetUserByIdAsync(id, Role.Teacher);
		return Results.Ok(result);
	}

	[Route("/email/{email}")]
	[Authorize]
	[HttpGet]
	public async Task<IResult> GetByEmail(string email)
	{
		var result = await _userService.GetUserByEmailAsync(email, Role.Teacher);
		return Results.Ok(result);
	}
}
