using System.Text;
using ezdomawka.Controllers.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ezdomawka.Controllers;

public class BaseController : Controller
{
    public IActionResult SingeElementInformation(string text)
    {
        return GenerateInformation(new HomeInformationExtensions.InfoElement(true, text));
    }
    
    
    public IActionResult MultiElementInformation(string head, string subHead, string body)
    {
        return GenerateInformation(new HomeInformationExtensions.InfoElement(true, head),
            new HomeInformationExtensions.InfoElement(true, subHead),
            new HomeInformationExtensions.InfoElement(false, body));
    }
    
    public IActionResult MultiElementInformation(string head, string body)
    {
        return GenerateInformation(new HomeInformationExtensions.InfoElement(true, head),
            new HomeInformationExtensions.InfoElement(false, body));
    }

    
    public IActionResult GenerateInformation(params HomeInformationExtensions.InfoElement[] texts)
    {
        return View("../Home/Information", HomeInformationExtensions.GenerateInformation(texts));
    }
}