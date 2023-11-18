using System.Diagnostics;
using BP.Common.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using BP.WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace BP.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private List<Subject> _subjects;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Dashboard()
    {
        var dashboard = new DashboardModel();   
        
        return View();
    }
    
    [Authorize]
    public IActionResult Privacy()
    {
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}