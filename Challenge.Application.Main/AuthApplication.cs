using AutoMapper;
using Challenge.Application.DTO;
using Challenge.Application.Interface;
using Challenge.Domain.Interface;
using Challenge.Infrastructure.Interface;
using Challenge.Transversal.Common;

namespace Challenge.Application.Main
{
    public class AuthApplication : IAuthApplication
    {
        private readonly IAuthDomain _authDomain;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public AuthApplication(IAuthDomain authDomain, IMapper iMapper, IPasswordHasher passwordHasher)
        {
            _authDomain = authDomain;
            _mapper = iMapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<Response<UserDto>> Authenticate(string username, string password)
        {
            var response = new Response<UserDto>();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                response.IsSuccess = false;
                response.Message = "Username and password cannot be empty.";
                return response;
            }

            var usuario = await _authDomain.GetHashPasswordByUsername(username);
            if (usuario == null)
            {
                response.IsSuccess = false;
                response.Message = "Incorrect username and/or password.";
                return response;
            }

            var hashedPassword = usuario.UserPassword;
            var verifyPassword = _passwordHasher.VerifyHashedPassword(hashedPassword, password);
            if (!verifyPassword)
            {
                response.IsSuccess = false;
                response.Message = "Incorrect username and/or password.";
                return response;
            }

            try
            {
                var user = await _authDomain.Authenticate(username, hashedPassword);
                response.Data = _mapper.Map<UserDto>(user);
                response.IsSuccess = true;
                response.Message = "Authentication Successful.";
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = true;
                response.Message = "User does not exist.";
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<Response<bool>> SaveRefreshToken(int userId, string token, string refreshToken)
        {
            var response = new Response<bool>();
            try
            {
                response.Data = await _authDomain.SaveRefreshToken(userId, token, refreshToken);
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Se guardó el token correctamente!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ResponseRefreshToken<int>> VerifyRefreshToken(string expiredToken, string refreshToken, int userId)
        {
            var response = new ResponseRefreshToken<int>();

            if (string.IsNullOrEmpty(expiredToken) || string.IsNullOrEmpty(refreshToken))
            {
                response.Message = "Parámetros no pueden ser vacios.";
                return response;
            }

            try
            {
                response.Result = await _authDomain.VerifyRefreshToken(expiredToken, refreshToken, userId);
                if (response.Result == 1)
                {
                    response.IsSuccess = true;
                    response.Message = "Se refrescó el token correctamente!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }
    }
}
