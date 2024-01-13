using CloudShift.Domain;

namespace CloudShift.Application.Repositories
{
    public interface IBaseRepository<T> where T : class, IEntity
    {
        Task<IEnumerable<T>> GetAll();

        Task<T?> GetFirst();
    }
}
