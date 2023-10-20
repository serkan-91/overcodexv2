using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Mail;
using System.Net;
using overcodexv2.Models;

namespace overcodexv2.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public EmailModel? Email { get; set; }

       
     
        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Mail gönderme işlemleri
                SendMail(Email.From, Email.Subject, Email.Message);

                TempData["SuccessMessage"] = "Mail başarıyla gönderildi.";
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Mail gönderme hatası: " + ex.Message;
                return Page();
            }
        }

        private static void SendMail(string from, string subject, string message)
        {
            // Mail gönderme işlemleri
            // Bu örnekte, SMTP kullanarak basit bir mail gönderme işlemi gerçekleştirilmektedir.
            using SmtpClient smtpClient = new("smtp.gmail.com")
            {
                UseDefaultCredentials= false,
                Credentials   = new NetworkCredential("your-email@example.com", "your-password"),
                Port=587,
                EnableSsl=true 
        };
          
           

            MailMessage mailMessage = new()
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = message,
            };
            mailMessage.To.Add("mail@overcodex.com");

            smtpClient.Send(mailMessage);
        }
    }
} 