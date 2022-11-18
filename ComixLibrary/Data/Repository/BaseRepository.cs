using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ComixLibContext context;

    public BaseRepository(ComixLibContext context)
    {
        this.context = context;
    }

    public async Task AddAsync(T entity)
    {
        await context.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entitySet = context.Set<T>();
        var entity = await entitySet.FirstOrDefaultAsync(x => x.Id == id);
        
        if(entity is null)
        { 
            return false;
        }

        context.Remove<T>(entity);
        await context.SaveChangesAsync();
        return true;
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        var entityList = context.Set<T>();
        return Task.FromResult(entityList.AsEnumerable());
    }

    public async Task<T?> GetAsync(Guid id)
    {
        var entitySet = context.Set<T>();
        var entity = await entitySet.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            return null;
        }

        return entity;
    }

    public async Task<bool> IsExist(Guid id)
    {
        var entitySet = context.Set<T>();
        var entity = await entitySet.FirstOrDefaultAsync(x => x.Id == id);

        if (entity is null)
        {
            return false;
        }

        return true;
    }

    public async Task UpdateAsync(T entity)
    {
        var isExist = await IsExist(entity.Id);
        
        if (!isExist)
        {
            return;
        }

        context.Set<T>().Update(entity);
    }
}
