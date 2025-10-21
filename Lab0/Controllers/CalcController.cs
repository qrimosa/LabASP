using Lab0.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab0.Controllers;

public class CalcController : Controller
{
    // GET
    public IActionResult Form()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Result(CalcModel model)
    {
        if (!model.isValid())
        {
            return View("Error", "Can't do the calculations!");
        }
        ViewBag.Result = model.Result;
        return View("Calc");
    }
}