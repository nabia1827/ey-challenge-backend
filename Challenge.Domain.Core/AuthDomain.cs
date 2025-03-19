using Challenge.Domain.Entity;
using Challenge.Domain.Interface;
using Challenge.Infrastructure.Interface;

namespace Challenge.Domain.Core
{
    public class AuthDomain : IAuthDomain
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<User> Authenticate(string username, string password)
        {
            return await _unitOfWork.Auth.Authenticate(username, password);
        }

        public async Task<User> GetHashPasswordByUsername(string username)
        {
            return await _unitOfWork.Auth.GetHashPasswordByUsername(username);
        }

        public async Task<bool> SaveRefreshToken(int userId, string token, string refreshToken)
        {
            return await _unitOfWork.Auth.SaveRefreshToken(userId, token, refreshToken);
        }

        public async Task<int> VerifyRefreshToken(string expiredToken, string refreshToken, int userId)
        {
            return await _unitOfWork.Auth.VerifyRefreshToken(expiredToken, refreshToken, userId);
        }
    }
}
