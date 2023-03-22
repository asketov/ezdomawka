using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ezdomawka.Controllers.Extensions;

public static class HomeInformationExtensions
{
    public static string GenerateInformation(IEnumerable<InfoElement> texts)
    {
        StringBuilder information = new StringBuilder();
        
        foreach (var infoElement in texts)
        {
            if (infoElement.IsHead)
                information.Append(GenerateHeadString(infoElement.Text));
            else
                information.Append(GenerateBodyString(infoElement.Text));
        }

        return information.ToString();
    }

    
    public static string GenerateHeadString(string text)
    {
        return $"<div class=\"head\">{text}</div>";
    }

    public static string GenerateBodyString(string text)
    {
        return $"<div class=\"head\">{text}</div>";
    }
    
    
    public class InfoElement
    {
        public readonly bool IsHead;
        public readonly string Text;

        public InfoElement(bool isHead, string text)
        {
            IsHead = isHead;
            Text = text;
        }
    }
}