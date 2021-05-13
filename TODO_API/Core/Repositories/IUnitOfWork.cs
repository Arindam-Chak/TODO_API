using System.Threading.Tasks;

namespace ToDoAPI.Core.Repositories
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}