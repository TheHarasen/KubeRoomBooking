using AuthDomain.Entities;
using AuthDomain.Requests;

namespace AuthApplication.Abstracts;

public interface IUserRepository
{
	Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
	Task<User?> GetUserByIdAsync(Guid id);
	Task<User?> GetUserByEmailAsync(string email);
	Task EditUserAsync(User user);
	Task DeleteUserAsync(Guid id);
	Task<IEnumerable<User>> GetUsersAsync(Role role);
	Task<int> SaveChangesAsync();
}
