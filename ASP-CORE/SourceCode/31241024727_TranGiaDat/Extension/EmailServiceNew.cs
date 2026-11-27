using System.Net;
using System.Net.Mail;
using System.Text;

namespace DatPaper.Extension;

public static class EmailServiceNew
{
    public static async Task<bool> SendMailGoogleSmtp(
        string gmail,
        string appPassword,
        string recipient,
        string subject,
        string body)
    {
        if (string.IsNullOrWhiteSpace(gmail)
            || string.IsNullOrWhiteSpace(appPassword))
        {
            return false;
        }

        try
        {
            using var client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                    gmail, appPassword.Replace(" ", ""))
            };

            using var message = new MailMessage
            {
                From = new MailAddress(gmail, "Đạt Paper"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
                BodyEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8
            };

            message.To.Add(new MailAddress(recipient));

            using var timeout =
                new CancellationTokenSource(TimeSpan.FromSeconds(25));

            await client.SendMailAsync(message, timeout.Token);
            return true;
        }
        catch
        {
            Console.Error.WriteLine(
                "Không gửi được email xác nhận Đạt Paper.");

            return false;
        }
    }
}