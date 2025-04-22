using AuthApplication.Abstracts;
using AuthDomain.Entities;
using AuthDomain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers;
[Authorize]
[ApiController]
[Route("self")]
public class SelfController : ControllerBase
{
	private readonly IAccountService _accountService;
	private readonly IUserService _userService;

	public SelfController(IAccountService accountService, IUserService userService)
	{
		_accountService = accountService;
		_userService = userService;
	}

	[HttpPut]
	public async Task<IResult> EditSelf(EditSelfRequest request)
	{
		await _userService.EditSelfAsync(request);
		return Results.Ok();
	}

	[HttpDelete]
	public async Task<IResult> DeleteSelf(DeleteSelfRequest request)
	{
		await _userService.DeleteSelfAsync(request);
		return Results.Ok();
	}
}
