using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyWebApplication.Models;



namespace MyWebApplication.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Crochet(){
        return View();
    }
    
    public IActionResult Photography(){
        return View();
    }

    // public IActionResult SoftwarePrjs(){
    //     return View();
    // }
    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Contact(){
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    // public IActionResult DisplayProfileImage(string imageName){
    //     string imagePath = Server.MapPath("~/Images/"+imageName);
    //     return File(imagePath, "image/jpeg");
    // }
}
