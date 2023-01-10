using Core.Domain;

namespace Core.Data;

public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<bool> IsExist(int id);
}
