using Core.Data;
using Core.Domain;
using Core.Filters;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

public class SearchController : Controller
{
    private readonly IUnitOfWork unitOfWork;
    
    private string author = string.Empty;
    private string genre = string.Empty;
    private int yearOfPublish = 0;
    private string name = string.Empty;

    public SearchController(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> Index(string filter)
    {
        name = filter;
        var comixes = await GetComixesWithFilters();
        return View("Index", comixes);
    }

    [HttpPost]
    public async Task<IActionResult> Search(ComixFilter filter)
    {
        author = filter.Author;
        genre = filter.Genre;
        yearOfPublish = filter.YearOfPublishing;
        name = filter.Name;
        return await Index(name);
    }

    private  async Task<IEnumerable<Comix>> GetComixesWithFilters()
    {
        var comixes = await unitOfWork.ComixRepository.GetAllAsync();
        comixes = string.IsNullOrEmpty(author) ? comixes : comixes.Where(x => x.Author == author);
        comixes = string.IsNullOrEmpty(genre) ? comixes : comixes.Where(x => x.Genre == genre);
        comixes = yearOfPublish is 0 ? comixes : comixes.Where(x => x.YearOfPublishing == yearOfPublish);
        comixes = string.IsNullOrEmpty(name) ? comixes : comixes.Where(x => x.Name.Contains(name));
        return comixes;
    }
}
