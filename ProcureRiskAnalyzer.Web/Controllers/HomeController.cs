using Microsoft.AspNetCore.Mvc;

namespace ProcureRiskAnalyzer.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Title = "ProcureRiskAnalyzer";
        return View();
    }
}
