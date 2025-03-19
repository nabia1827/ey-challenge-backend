using Challenge.Application.DTO;
using Challenge.Transversal.Common;

namespace Challenge.Application.Interface
{
    public interface IAuthApplication
    {
        Task<Response<UserDto>> Authenticate(string username, string password);
        Task<Response<bool>> SaveRefreshToken(int userId, string token, string refreshToken);
        Task<ResponseRefreshToken<int>> VerifyRefreshToken(string expiredToken, string refreshToken, int userId);
    }
}
