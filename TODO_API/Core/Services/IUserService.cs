using System.Threading.Tasks;
using ToDoAPI.Core.Models;
using ToDoAPI.Core.Services.Communication;

namespace ToDoAPI.Core.Services
{
    public interface IUserService
    {
         Task<CreateUserResponse> CreateUserAsync(User user, params ApplicationRole[] userRoles);
         Task<User> FindByEmailAsync(string email);
    }
}