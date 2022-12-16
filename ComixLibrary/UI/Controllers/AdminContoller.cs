using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

public class AdminContoller : Controller
{
    private bool isLogin = false;
    
    public ActionResult Index()
    {
        return View();
    }

    private void Login()
    {
        isLogin = true;
    }
}
