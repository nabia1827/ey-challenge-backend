using Challenge.Domain.Entity;

namespace Challenge.Domain.Interface
{
    public interface IAuthDomain
    {
        Task<User> Authenticate(string username, string password);
        Task<User> GetHashPasswordByUsername(string username);
        Task<bool> SaveRefreshToken(int userId, string token, string refreshToken);
        Task<int> VerifyRefreshToken(string expiredToken, string refreshToken, int userId);
    }
}
