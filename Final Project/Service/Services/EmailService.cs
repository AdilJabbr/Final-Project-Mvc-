using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit;
using MimeKit.Text;
using MimeKit.Text;
using Service.Helpers;
using Service.Services.Interfaces;
using System.Net;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly IConfiguration _configuration;


    public EmailService(IOptions<EmailSettings> options , IConfiguration configuration)
    {
        _emailSettings = options.Value;
        _configuration = configuration;
    }

    public void Send(string to, string subject, string html, string from = null)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from ?? _emailSettings.From));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = html };

        // send email
        using var smtp = new SmtpClient();
        smtp.Connect(_emailSettings.Server, _emailSettings.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(from ?? _emailSettings.From, _emailSettings.Password);
        smtp.Send(email);
        smtp.Disconnect(true);
    }

    public void SendEmail(string toEmail, string subject, string emailBody)
    {
        //// Set up SMTP client
        //SmtpClient client = new SmtpClient(_configuration["EmailSettings:Smtp"], int.Parse(_configuration["EmailSettings:Port"]));
        //client.EnableSsl = true;
        //client.UseDefaultCredentials = false;
        //client.Credentials = new NetworkCredential(_configuration["EmailSettings:Host"], _configuration["EmailSettings:Password"]);

        //// Create email message
        //MailMessage mailMessage = new MailMessage();
        //mailMessage.From = new MailAddress(_configuration["EmailSettings:Host"]);
        //mailMessage.To.Add(toEmail);
        //mailMessage.Subject = subject;
        //mailMessage.IsBodyHtml = true;


        //mailMessage.Body = emailBody.ToString();

        //// Send email
        //client.Send(mailMessage);
    }
}