namespace Challenge.Services.WebApi.Models.Requests
{
    public class ResetPasswordRequest
    {
        public string Otp { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public bool? isStrong { get; set; }
    }
}
