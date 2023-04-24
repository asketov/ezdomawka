using System.Text;
using ezdomawka.Controllers.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ezdomawka.Controllers;

public class BaseController : Controller
{
    public string GetRedirectLink(string? text)
    {
        return text ?? "/home/index";
    }

    public IActionResult SomeSingWrongMessage()
    {
        return SingeElementInformation("Что-то пошло не так, попробуйте позже");
    }
    public IActionResult SingeElementInformation(string text)
    {
        return GenerateInformation(new HomeInformationExtensions.InfoElement(true, text));
    }
    
    
    public IActionResult MultiElementInformation(string head, string prebody, string body)
    {
        return GenerateInformation(new HomeInformationExtensions.InfoElement(true, head),
            new HomeInformationExtensions.InfoElement(false, prebody),
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