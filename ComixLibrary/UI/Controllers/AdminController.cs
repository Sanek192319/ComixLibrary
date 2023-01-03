using Core.Business;
using Core.Data;
using Core.Domain;
using Core.Options;
using Microsoft.AspNetCore.Mvc;
using UI.Models;

namespace UI.Controllers;

public class AdminController : Controller
{

    public AdminController(IFileService fileService, FileSettings fileSettings, IUnitOfWork unitOfWork)
    {
        this.fileService = fileService;
        this.fileSettings = fileSettings;
        this.unitOfWork = unitOfWork;
    }

    private readonly IFileService fileService;
    private readonly FileSettings fileSettings;
    private readonly IUnitOfWork unitOfWork;

    public ActionResult Index()
    {
        if (string.IsNullOrEmpty(ViewBag.Message))
        {
            ViewBag.Message = "Введіть пароль";
        }
        return View();
    }


    [HttpPost]
    public async Task<ActionResult> Login(string login, string pass)
    {
        var admin = await unitOfWork.AdminRepository.GetAllAsync();
        if (admin.First().Login == login && admin.First().Password == pass)
        {
            return RedirectToAction("Choose");
        }
        else
        {
            return RedirectToAction("Index");
        }
    }

    public IActionResult Choose()
    {
        return View();
    }


    public  IActionResult AddComix()
    {
        return View();
    }

    public async Task<ActionResult> DeleteComix([FromRoute] Guid id)
    {
        await unitOfWork.ComixRepository.DeleteAsync(id);
        return RedirectToAction("Index", controllerName: "Home");
    }

    public ActionResult EditComix([FromRoute] Comix comix)
    {
        return View(comix);
    }

    [HttpPost]
    public async Task<IActionResult> SaveComix([FromForm] AddComixViewModel comix)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        await fileService.UploadFile(comix.DocFile.OpenReadStream(), path + fileSettings.FilePath + comix.DocFile.FileName);
        await fileService.UploadFile(comix.Photo.OpenReadStream(), path + fileSettings.PhotoPath + comix.Photo.FileName);
        
        await unitOfWork.ComixRepository.AddAsync(new Comix
        {
            Author = comix.Author,
            Name = comix.Name,
            Description = comix.Description,
            YearOfPublishing = comix.YearOfPublish,
            FilePath = fileSettings.FilePath + comix.DocFile.FileName,
            PhotoPath = fileSettings.PhotoPath + comix.Photo.FileName,
            Genre = comix.Genre
        });
        
        return RedirectToAction("Index");
    }

    [HttpPut]
    private async Task UpdateComix([FromForm] UpdateComixViewModel comix)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        await fileService.UploadFile(comix.DocFile.OpenReadStream(), path + fileSettings.FilePath + comix.DocFile.FileName);
        await fileService.UploadFile(comix.Photo.OpenReadStream(), path + fileSettings.PhotoPath + comix.Photo.FileName);
        await unitOfWork.ComixRepository.UpdateAsync(new Comix
        {
            Id = Guid.Parse(comix.Id),
            Author = comix.Author,
            Name = comix.Name,
            Description = comix.Description,
            YearOfPublishing = comix.YearOfPublish,
            FilePath = fileSettings.FilePath + comix.DocFile.FileName,
            PhotoPath = fileSettings.PhotoPath + comix.Photo.FileName,
            Genre = comix.Genre
        });
    }

    public async Task<IActionResult> List()
    {
        var comixes = await unitOfWork.ComixRepository.GetAllAsync();
        return View(comixes);
    }

}
