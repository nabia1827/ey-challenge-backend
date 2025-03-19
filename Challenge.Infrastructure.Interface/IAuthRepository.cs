using Challenge.Domain.Entity;

namespace Challenge.Infrastructure.Interface
{
    public interface IAuthRepository
    {
        Task<User> Authenticate(string username, string password);
        Task<User> GetHashPasswordByUsername(string username);
        Task<bool> SaveRefreshToken(int userId, string token, string refreshToken);
        Task<int> VerifyRefreshToken(string expiredToken, string refreshToken, int userId);
    }
}
