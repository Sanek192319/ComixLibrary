using Core.Business;
using Core.Data;
using Core.Domain;
using Core.Enums;
using Core.Filters;
using System.Text.RegularExpressions;

namespace Business.Services;

public class ComixService : IComixService
{
    private readonly IUnitOfWork unitOfWork;

    public ComixService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> AddComix(Comix comix)
    {
        comix.Id = Guid.NewGuid();
        await unitOfWork.ComixRepository.AddAsync(comix);
        return await unitOfWork.ComixRepository.IsExist(comix.Id);
    }

    public async Task<bool> DeleteComix(Guid id)
    {
        await unitOfWork.ComixRepository.DeleteAsync(id);
        return await unitOfWork.ComixRepository.IsExist(id);
    }

    public async Task<IEnumerable<Comix>> GetComixes(PaginationFilter paginationFilter, ComixFilter comixFilter = default, SortFilter sortFilter = default)
    {
        var comixes = await unitOfWork.ComixRepository.GetAllAsync();
        
        if (sortFilter is not null)
        {
            comixes = sortFilter.Order == Order.Ascending ? comixes.OrderBy(x => x.Name) : comixes.OrderByDescending(x => x.Name);
        }

        if (comixFilter is not null)
        {
            if (comixFilter.Name is not null)
            {
                comixes = comixes.Where(x => Regex.IsMatch(x.Name, $"*{comixFilter.Name}"));
            }

            if (comixFilter.Author is not null)
            {
                comixes = comixes.Where(x => Regex.IsMatch(x.Author, $"*{comixFilter.Author}"));
            }

            if (comixFilter.YearOfPublishing != default)
            {
                comixes = comixes.Where(x => x.YearOfPublishing == comixFilter.YearOfPublishing);
            }

            if (comixFilter.Genre is not null)
            {
                comixes = comixes.Where(x => x.Genre == comixFilter.Genre);
            }
        }

        comixes = comixes.Skip(paginationFilter.Size * paginationFilter.NumberOfPage - 1);
        comixes = comixes.Take(paginationFilter.Size);

        return comixes;
    }

    public async Task<bool> UpdateComix(Guid id, Comix comix)
    {
        if (await unitOfWork.ComixRepository.IsExist(id))
        {
            await unitOfWork.ComixRepository.UpdateAsync(comix);
            return true;
        }

        return false;
    }
}
