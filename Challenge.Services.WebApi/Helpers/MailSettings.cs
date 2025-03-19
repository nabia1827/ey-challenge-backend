namespace Challenge.Services.WebApi.Helpers
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string Pass { get; set; }
        public string? Cc { get; set; }
        public string? Bcc { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool? enableSsl { get; set; }
    }
}
