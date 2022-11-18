using Core.Domain;

namespace Core.Data;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<bool> IsExist(Guid id);
}
