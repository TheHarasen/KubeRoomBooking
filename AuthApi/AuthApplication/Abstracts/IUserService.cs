using AuthDomain.Entities;
using AuthDomain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApplication.Abstracts;

public interface IUserService
{
	Task<User?> GetUserByIdAsync(Guid id, Role role);
	Task<User?> GetUserByEmailAsync(string email, Role role);
	Task EditUserAsync(EditRequest request, Role role);
	Task EditSelfAsync(EditSelfRequest request);
	Task DeleteUserAsync(Guid id, Role role);
	Task DeleteSelfAsync(DeleteSelfRequest request);
	Task<IEnumerable<User>> GetUsersAsync(Role role);
}
