using AuthApplication.Abstracts;
using AuthDomain.Entities;
using AuthDomain.Exceptions.User;
using AuthDomain.Requests;
using Microsoft.AspNetCore.Identity;

namespace AuthApplication.Services;

public class UserService : IUserService
{
	private readonly IUserRepository _userRepo;
	private readonly UserManager<User> _userManager;

	public UserService(IUserRepository userRepo, UserManager<User> userManager)
	{
		_userRepo = userRepo;
		_userManager = userManager;
	}

	public async Task<User?> GetUserByIdAsync(Guid id, Role role)
	{
		if (id == Guid.Empty)
			throw new Exception("User Id is empty");

		var user = await _userRepo.GetUserByIdAsync(id);

		if (user == null)
			throw new UserIdDosentExistException(id.ToString());

		if (user.Role != role)
			throw new UserNotRightRoleException(role, user.Role);

		return user;
	}

	public async Task<User?> GetUserByEmailAsync(string email, Role role)
	{
		if (string.IsNullOrEmpty(email))
			throw new Exception("User Email is empty");

		var user = await _userRepo.GetUserByEmailAsync(email);

		if (user == null)
			throw new UserEmailDosentExistException(email);

		if (user.Role != role)
			throw new UserNotRightRoleException(role, user.Role);

		return user;
	}

	public async Task EditUserAsync(EditRequest request, Role role)
	{
		var user = await GetUserByIdAsync(request.Id, role);

		if (user == null)
			throw new UserIdDosentExistException(request.Id.ToString());

		if (user.Role != role)
			throw new UserNotRightRoleException(role, user.Role);

		user.Email = request.Email;
		user.FirstName = request.FirstName;
		user.LastName = request.LastName;
		user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Password);

		await _userRepo.EditUserAsync(user!);
		await _userRepo.SaveChangesAsync();
	}
	public async Task EditSelfAsync(EditSelfRequest request)
	{
		if (request.Id == Guid.Empty)
			throw new Exception("User Id is empty");

		var user = await _userRepo.GetUserByIdAsync(request.Id);

		if (user == null)
			throw new UserIdDosentExistException(request.Id.ToString());

		if (!await _userManager.CheckPasswordAsync(user, request.PasswordOld))
			throw new UserWrongPassword();

		user.Email = request.Email;
		user.FirstName = request.FirstName;
		user.LastName = request.LastName;
		user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.PasswordNew);

		await _userRepo.EditUserAsync(user!);
		await _userRepo.SaveChangesAsync();
	}

	public async Task DeleteUserAsync(Guid id, Role role)
	{
		if (id == Guid.Empty)
			throw new Exception("User Id is empty");

		var user = await _userRepo.GetUserByIdAsync(id);

		if (user == null)
			throw new UserIdDosentExistException(id.ToString());

		if (user.Role != role)
			throw new UserNotRightRoleException(role, user.Role);

		await _userRepo.DeleteUserAsync(id);
		await _userRepo.SaveChangesAsync();
	}

	public async Task DeleteSelfAsync(DeleteSelfRequest request)
	{
		var user = await _userManager.FindByEmailAsync(request.Email);

		if (user == null)
			throw new UserEmailDosentExistException(request.Email);

		if (!await _userManager.CheckPasswordAsync(user, request.Password))
			throw new UserWrongPassword();

		await _userRepo.DeleteUserAsync(request.Id);
		await _userRepo.SaveChangesAsync();
	}


	public async Task<IEnumerable<User>> GetUsersAsync(Role role)
	{
		return await _userRepo.GetUsersAsync(role);
	}
}