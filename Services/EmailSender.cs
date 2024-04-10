using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace COMP2139_Assignment1.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _sendGridKey;
        public EmailSender(IConfiguration configuration)
        {
            _sendGridKey = configuration["SendGrid:ApiKey"];
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var client = new SendGridClient(_sendGridKey);
                var from = new EmailAddress("elio.fezollari@georgebrown.ca", "Project Collaborator");
                var to = new EmailAddress(email);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
                await client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as per your application's requirements
                Console.WriteLine($"Error occurred while sending email: {ex.Message}");
                throw; // rethrow the exception to propagate it further if needed
            }
        }
    }
}
