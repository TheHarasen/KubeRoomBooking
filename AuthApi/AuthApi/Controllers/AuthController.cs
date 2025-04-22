using AuthApi.DTOs;
using AuthApplication.Abstracts;
using AuthDomain.Requests;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;
[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
	private readonly IAccountService _accountService;

	public AuthController(IAccountService accountService)
	{
		_accountService = accountService;
	}

	[HttpPost]
	[Route("login")]
	public async Task<IResult> Login(LoginRequest request)
	{
		var result = await _accountService.LoginAsync(request);
		var response = new LoginResponse(result.accessToken, result.user.Id, result.user.FirstName, result.user.LastName, result.user.Email!);
		return Results.Ok(response);
	}

	[Authorize]
	[HttpPost]
	[Route("refresh")]
	public async Task<IResult> Refresh()
	{
		var refreshToken = Request.Cookies["REFRESH_TOKEN"];
		var result = await _accountService.RefreshTokenAsync(refreshToken);
		return Results.Ok(new LoginResponse(result.accessToken, result.user.Id, result.user.FirstName, result.user.LastName, result.user.Email!));
	}

	[Authorize]
	[HttpPost]
	[Route("logout")]
	public async Task<IResult> LogOut()
	{
		var refreshToken = Request.Cookies["REFRESH_TOKEN"];
		await _accountService.LogOutAsync(refreshToken);
		return Results.Ok();
	}
}
