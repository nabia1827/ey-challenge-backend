namespace Challenge.Services.WebApi.Models.Requests
{
    public class StrongPasswordRequest
    {
        public int UsuarioId { get; set; }
        public string? Password { get; set; }
    }
}
