using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyWebApplication.Models;

namespace MyWebApplication.Controllers;

public class ContactController : Controller{
    public IActionResult Index(){
        return View();
    }
}