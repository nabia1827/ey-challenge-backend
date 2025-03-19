namespace Challenge.Services.WebApi.Models.Requests
{
    public class RefreshTokenRequest
    {
        public int? UserId { get; set; }
        public string ExpiredToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
