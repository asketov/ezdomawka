﻿namespace BLL.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string text, string? link);
}