using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Supports
{
    public class SendMail
    {
        private IConfiguration configuration;
        public SendMail(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public bool Send(string to, string subject, string content)
        {
            try
            {
                var host = configuration["Gmail:host"];
                var port = int.Parse(configuration["Gmail:port"]);
                var username = configuration["Gmail:username"];
                var password = configuration["Gmail:password"];
                var enable = bool.Parse(configuration["Gmail:SMTP:starttls:enable"]);
                SmtpClient smtpClient = new SmtpClient
                {
                    Host = host,
                    Port = port,
                    EnableSsl = enable,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(username, password)
                };
                MailMessage mailMessage = new MailMessage(username, to);
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.Subject = subject;
                mailMessage.Body = content;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

    //public class SendMail
    //{
    //    private IConfiguration configuration;
    //    public SendMail(IConfiguration _configuration)
    //    {
    //        configuration = _configuration;
    //    }

    //    public bool Send(string to, string subject, string content, IFormFile formFile)
    //    {
    //        try
    //        {
    //            string pathfile = "" + formFile.FileName;

    //            var host = configuration["Gmail:host"];
    //            var port = int.Parse(configuration["Gmail:port"]);
    //            var username = configuration["Gmail:username"];
    //            var password = configuration["Gmail:password"];
    //            var enable = bool.Parse(configuration["Gmail:SMTP:starttls:enable"]);
    //            SmtpClient smtpClient = new SmtpClient
    //            {
    //                Host = host,
    //                Port = port,
    //                EnableSsl = enable,
    //                Credentials = new NetworkCredential(username, password)
    //            };
    //            MailMessage mailMessage = new MailMessage(username, to);
    //            mailMessage.BodyEncoding = Encoding.UTF8;
    //            mailMessage.Subject = subject;
    //            mailMessage.Body = content;
    //            mailMessage.IsBodyHtml = true;
    //            mailMessage.Priority = MailPriority.High;
    //            mailMessage.Attachments.Add(new Attachment(pathfile, MediaTypeNames.Application.Octet));
    //            smtpClient.Send(mailMessage);
    //            return true;
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //    }
    //}
}
