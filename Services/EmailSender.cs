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
                var from = new EmailAddress("aumzaveri06@gmail.com", "NorthPole");
                var to = new EmailAddress(email);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
                var response = await client.SendEmailAsync(msg);

				if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
				{
					Console.WriteLine("Email sent successfully!");
				}
				else
				{
					Console.WriteLine($"Failed to send email. Status code: {response.StatusCode}");
					string responseContent = await response.Body.ReadAsStringAsync();
					Console.WriteLine($"Response content: {responseContent}");
				}
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
