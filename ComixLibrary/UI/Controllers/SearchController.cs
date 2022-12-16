using Core.Data;
using Core.Domain;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class SearchController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        
        private string Author = string.Empty;
        private string Genre = string.Empty;
        private int YearOfPublish = 0;
        private string Name = string.Empty;

        public SearchController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var comixes = await unitOfWork.ComixRepository.GetAllAsync();
            return View(comixes);
        }

        [HttpPost]
        public async Task<IActionResult> Search()
        {
            var comixes = await GetComixesWithFilters();
            return View(comixes);
        }

        private  async Task<IEnumerable<Comix>> GetComixesWithFilters()
        {
            var comixes = await unitOfWork.ComixRepository.GetAllAsync();
            comixes = string.IsNullOrEmpty(Author) ? comixes : comixes.Where(x => x.Author == Author);
            comixes = string.IsNullOrEmpty(Genre) ? comixes : comixes.Where(x => x.Genre == Genre);
            comixes = YearOfPublish is 0 ? comixes : comixes.Where(x => x.YearOfPublishing.Year == YearOfPublish);
            comixes = string.IsNullOrEmpty(Name) ? comixes : comixes.Where(x => x.Name == Name);
            return comixes;
        }
    }
}
