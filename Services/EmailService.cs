using MimeKit;
using MailKit.Net.Smtp;
using BárdiHomework.Models;

namespace BárdiHomework.Services
{
    public class EmailService
    {
        public void SendConfirmationEmail(PaymentForm paymentForm)
        {
            
            var seatsList = paymentForm.Seats.Keys.ToList();
            string result = string.Join("", seatsList);
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("fakebboost@gmail.com"));
            email.To.Add(MailboxAddress.Parse(paymentForm.Email));
            email.Subject = "Seat Reservation";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                
                Text = $"Reservation has been made to seat(s): {result}"
                
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 465, MailKit.Security.SecureSocketOptions.SslOnConnect);
            smtp.Authenticate("fakebboost@gmail.com", "gconlzshnpusjupa");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
