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

    public async Task<ActionResult> DeleteComix([FromRoute] int id)
    {
        await unitOfWork.ComixRepository.DeleteAsync(id);
        return RedirectToAction("Index", controllerName: "Home");
    }

    public async Task<ActionResult> EditComix([FromRoute] int id)
    {
        var comix = await unitOfWork.ComixRepository.GetAsync(id);
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

    [HttpPost]
    public async Task<IActionResult> UpdateComix([FromForm] UpdateComixViewModel comix)
    {

        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var oldComix = await unitOfWork.ComixRepository.GetAsync(int.Parse(comix.Id));

        var newComix = new Comix
        {
            Id = int.Parse(comix.Id),
            Author = comix.Author,
            Name = comix.Name,
            Description = comix.Description,
            YearOfPublishing = comix.YearOfPublish,
            Genre = comix.Genre
        };

        if (comix.Photo is null)
        {
            newComix.PhotoPath = oldComix.PhotoPath;
        }
        else
        {
            await fileService.UploadFile(comix.Photo.OpenReadStream(), path + fileSettings.PhotoPath + comix.Photo.FileName);
            newComix.PhotoPath = fileSettings.PhotoPath + comix.Photo.FileName;
        }

        if (comix.DocFile is null)
        {
            newComix.FilePath = oldComix.FilePath;
        }
        else
        {
            await fileService.UploadFile(comix.DocFile.OpenReadStream(), path + fileSettings.FilePath + comix.DocFile.FileName);
            newComix.FilePath = fileSettings.FilePath + comix.DocFile.FileName;
        }

        await unitOfWork.ComixRepository.UpdateAsync(newComix);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> List()
    {
        var comixes = await unitOfWork.ComixRepository.GetAllAsync();
        return View(comixes);
    }

}
