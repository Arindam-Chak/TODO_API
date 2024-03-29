using System.Threading.Tasks;
using ToDoAPI.Core.Models;

namespace ToDoAPI.Core.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user, ApplicationRole[] userRoles);
        Task<User> FindByEmailAsync(string email);
    }
}