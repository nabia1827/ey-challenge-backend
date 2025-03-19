using Challenge.Services.WebApi.Helpers;
using Challenge.Services.WebApi.Models.Requests;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using System.Net;

namespace Challenge.Services.WebApi.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            MailMessage newMessage = new MailMessage();
            System.Net.Mail.SmtpClient mailService = new System.Net.Mail.SmtpClient();
            newMessage.From = new MailAddress(_mailSettings.Mail);

            //Cargando datos desde mailRequest
            newMessage.To.Add(mailRequest.ToEmail);
            if (mailRequest.ToCc != null)
            {
                foreach (var ccmail in mailRequest.ToCc)
                {
                    if (ccmail != String.Empty)
                    {
                        newMessage.CC.Add(MailboxAddress.Parse(ccmail).ToString());
                    }
                }
            }
            newMessage.Subject = mailRequest.Subject;
            newMessage.Body = mailRequest.Body;


            //Configuracion base para enviar el email
            mailService.Port = 25;
            mailService.EnableSsl = true;
            mailService.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailService.Host = _mailSettings.Host;
            mailService.UseDefaultCredentials = false;
            mailService.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Pass);
            mailService.Send(newMessage);
        }

        public async Task SendEmailAsyncV2(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.Cc.Add(MailboxAddress.Parse(mailRequest.ToEmail));

            if (mailRequest.ToCc != null)
            {
                foreach (var username in mailRequest.ToCc)
                {
                    if (username != String.Empty && username != null)
                        email.To.Add(MailboxAddress.Parse(username));
                }
            }

            //email.Bcc.Add(MailboxAddress.Parse(_mailSettings.Bcc));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Pass);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendCustomizedEmailAsync(string emailDestino, string emailSubject, string emailBody, IEnumerable<string> emailToCC = null, List<IFormFile> emailAttachments = null)
        {
            MailRequest request = new MailRequest()
            {
                ToEmail = emailDestino,
                Subject = emailSubject,
                Body = emailBody,
                ToCc = emailToCC,
            };

            //if (request.ToCc != null)
            //{
            //    request.ToCc = StringHelper.RemoveWhitespace(_mailSettings.Cc).Split(";");
            //}

            if (emailAttachments != null)
            {
                request.Attachments = emailAttachments;
            }

            await SendEmailAsyncV2(request);
        }
    }
}
