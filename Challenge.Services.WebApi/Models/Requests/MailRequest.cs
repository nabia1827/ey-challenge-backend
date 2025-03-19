namespace Challenge.Services.WebApi.Models.Requests
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public IEnumerable<string>? ToCc { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public List<IFormFile>? Attachments { get; set; }
        public List<string>? AttachmentsUrl { get; set; }
        public string? Type { get; set; }
    }
}
