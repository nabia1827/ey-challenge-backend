using Challenge.Services.WebApi.Models.Requests;

namespace Challenge.Services.WebApi.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendEmailAsyncV2(MailRequest mailRequest);
        Task SendCustomizedEmailAsync(string emailDestino, string emailSubject, string emailBody, IEnumerable<string> emailToCC = null, List<IFormFile> emailAttachments = null);
    }
}
