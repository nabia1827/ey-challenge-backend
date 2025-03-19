using System.Text.Json.Serialization;

namespace Challenge.Application.DTO
{
    public class RefreshTokenDto
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Active { get; set; }
        public int Invalid { get; set; }

        [JsonInclude]
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
