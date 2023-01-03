using Core.Business;
using Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class ComixController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFileService fileService;

        public ComixController(IUnitOfWork unitOfWork, IFileService fileService)
        {
            this.unitOfWork = unitOfWork;
            this.fileService = fileService;
        }
        
        [HttpGet("/Comix/{id}")]
        public async Task<IActionResult> Index([FromRoute] string id)
        {
            var comix = await unitOfWork.ComixRepository.GetAsync(Guid.Parse(id));
            return View(comix);
        }

        [HttpGet("/Comix/{id}/GetFile")]
        public async Task<IActionResult> GetFile([FromRoute] string id)
        {
            var comix = await unitOfWork.ComixRepository.GetAsync(Guid.Parse(id));

            string fileType = "application/octet-stream";
            var fileInfo = new FileInfo(comix.FilePath);

            var path = Path.Combine(Directory.GetCurrentDirectory(), comix.FilePath);

            return File(path, fileType, fileInfo.Name);
        }

        [HttpGet("/Comix/{id}/ReadFile")]
        public async Task<IActionResult> ReadFile([FromRoute] string id)
        {
            var comix = await unitOfWork.ComixRepository.GetAsync(Guid.Parse(id));

            string fileType = "application/pdf";
            var fileInfo = new FileInfo(comix.FilePath);

            var path = Path.Combine(Directory.GetCurrentDirectory(), comix.FilePath);

            return File(path, fileType);
        }
    }
}
