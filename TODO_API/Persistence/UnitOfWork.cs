using System.Threading.Tasks;
using ToDoAPI.Core.Repositories;

namespace ToDoAPI.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();

        }
      

    }
}