using CloudShift.Domain;
using Microsoft.EntityFrameworkCore;

namespace CloudShift.Application.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntity
    {
        private readonly CloudShiftDbContext _dbContext;
        private readonly DbSet<T> DbSet;

        public BaseRepository(CloudShiftDbContext dbContext)
        {
            _dbContext = dbContext;
            DbSet = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await DbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetFirst()
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
