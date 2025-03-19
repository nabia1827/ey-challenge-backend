using Challenge.Application.DTO;
using Challenge.Application.Interface;
using Challenge.Services.WebApi.Helpers;
using Challenge.Services.WebApi.Models.Requests;
using Challenge.Transversal.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Challenge.Services.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthApplication _authApplication;
        private readonly AppSettings _appSettings;

        public AuthController(IAuthApplication authApplication, IOptions<AppSettings> appSettings)
        {
            _authApplication = authApplication;
            _appSettings = appSettings.Value;
        }


        private string BuildToken(Response<UserDto> usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {

                    new Claim("UserId", usuario.Data.UserId.ToString()),
                    new Claim("Username", usuario.Data.Username),
                    new Claim("UserFirstName", usuario.Data.UserFirstName),
                    new Claim("UserLastName", usuario.Data.UserLastName),

                    new Claim("ProfileId", usuario.Data.ProfileId.ToString()),
                    new Claim("UserEmail", usuario.Data.UserEmail.ToString()),
                    new Claim("UserImage", usuario.Data.UserImage??""),

                }),
                Expires = DateTime.UtcNow.AddHours(3),
                //Expires = DateTime.UtcNow.AddSeconds(30), //for test
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        private string BuildRefreshToken()
        {
            var byteArray = new byte[64];
            var refreshToken = "";

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteArray);
                refreshToken = Convert.ToBase64String(byteArray);
            }
            return refreshToken;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationRequest usuario)
        {
            var response = await _authApplication.Authenticate(usuario.Username, usuario.UserPassword);

            if (response.IsSuccess)
            {
                if (response.Data != null)
                {
                    response.Data.Token = BuildToken(response);
                    response.Data.RefreshToken = BuildRefreshToken();

                    var responseRefreshToken = _authApplication.SaveRefreshToken(response.Data.UserId, response.Data.Token, response.Data.RefreshToken);

                    if (responseRefreshToken != null)
                    {
                        return Ok(response);
                    }
                    else
                    {
                        // Manejo de error al guardar el token de actualización
                        return StatusCode(500, "Error saving refresh token.");
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            else if (response.IsSuccess == false)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var suposedTokenExpired = tokenHandler.ReadJwtToken(token.ExpiredToken);
            if (suposedTokenExpired.ValidTo > DateTime.UtcNow)
                return BadRequest(new Response<RefreshTokenDto> { IsSuccess = false, Message = "El token aún no ha expirado" });

            var claims = suposedTokenExpired.Claims;

            int userIdToken = int.Parse(claims.First(x => x.Type == "UsuId").Value);


            var user = new Response<UserDto>
            {
                Data = new UserDto
                {
                    UserId = int.Parse(claims.First(x => x.Type == "UserId").Value),
                    UserFirstName = claims.First(x => x.Type == "UserFirstName").Value,
                    Username = claims.First(x => x.Type == "Username").Value,
                    ProfileId = int.Parse(claims.First(x => x.Type == "ProfileId").Value),
                    UserEmail = claims.First(x => x.Type == "UserEmail").Value
                }
            };

            var response = await _authApplication.VerifyRefreshToken(token.ExpiredToken, token.RefreshToken, userIdToken);
            if (response.IsSuccess)
            {
                if (response.Result != 0)
                {
                    response.Token = BuildToken(user);
                    response.RefreshToken = BuildRefreshToken();
                    var responseRefreshToken = _authApplication.SaveRefreshToken(userIdToken, response.Token, response.RefreshToken);
                    return Ok(response);
                }
                else
                {
                    return Unauthorized();
                }
            }
            return BadRequest(response);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> LogOut([FromBody] RefreshTokenRequest data)
        {
            if (data.UserId <= 0 || data.UserId == null)
                return BadRequest();

            var response = await _authApplication.VerifyRefreshToken(data.ExpiredToken, data.RefreshToken, (int)data.UserId);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }


    }
}
