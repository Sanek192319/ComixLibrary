using Core.Domain;
using Core.Filters;

namespace Core.Business;

public interface IComixService
{
    Task<IEnumerable<Comix>> GetComixes(PaginationFilter paginationFilter, ComixFilter comixFilter, SortFilter sortFilter);
    Task<bool> AddComix(Comix comix);
    Task<bool> UpdateComix(Guid id, Comix comix);
    Task<bool> DeleteComix(Guid id);
}
