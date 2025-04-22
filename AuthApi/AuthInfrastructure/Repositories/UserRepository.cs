using AuthApplication.Abstracts;
using AuthDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthInfrastructure.Repositories;

public class UserRepository : IUserRepository
{
	private readonly ApplicationDbContext _applicationDbContext;

	public UserRepository(ApplicationDbContext applicationDbContext)
	{
		_applicationDbContext = applicationDbContext;
	}

	public async Task DeleteUserAsync(Guid id)
	{
		var delete = await _applicationDbContext.Users.FindAsync(id);
		if (delete != null)
			_applicationDbContext.Users.Remove(delete);
	}

	public async Task EditUserAsync(User user)
	{
		var edit = await _applicationDbContext.Users.FindAsync(user.Id);
		if (edit != null)
			edit.Copy(user);
	}

	public async Task<User?> GetUserByEmailAsync(string email)
	{
		return await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
	}

	public async Task<User?> GetUserByIdAsync(Guid id)
	{
		return await _applicationDbContext.Users.FindAsync(id);
	}

	public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
	{
		var user = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
		return user;
	}

	public async Task<IEnumerable<User>> GetUsersAsync(Role role)
	{
		return await _applicationDbContext.Users.Where(x => x.Role == role).ToListAsync();
	}

	public async Task<int> SaveChangesAsync()
	{
		return await _applicationDbContext.SaveChangesAsync();
	}
}
