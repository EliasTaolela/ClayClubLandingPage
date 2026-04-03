using Microsoft.AspNetCore.Identity.UI.Services;

namespace LandingPage.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // For now, do nothing (or log)
            Console.WriteLine($"Email to {email}: {subject}");
            return Task.CompletedTask;
        }
    }
}
