using Challenge.Services.WebApi.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Challenge.Services.WebApi.Modules.Authentication
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("Config");
            services.Configure<AppSettings>(appSettingsSection);

            // Configuracion de la Autenticacion Jwt
            var appSettings = appSettingsSection.Get<AppSettings>();

            //Secret Key en un array de Bytes. Para la validacion del Token se necesitan estos tres elementos
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var Issuer = appSettings.Issuer;
            var Audience = appSettings.Audience;

            //Declaramos que el token va a ser un Token de portador. Validamos el token que esta viniendo
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var claims = context.Principal.Claims;

                        var roles = claims.FirstOrDefault(c => c.Type == "ProfileId")?.Value;

                        if (!string.IsNullOrEmpty(roles))
                        {
                            var roleClaim = new Claim(ClaimTypes.Role, roles);
                            context.Principal.AddIdentity(new ClaimsIdentity(new[] { roleClaim }));
                        }

                        return Task.CompletedTask;
                    },

                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "True");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    //RequireExpirationTime = false,
                    ClockSkew = TimeSpan.Zero,
                    RoleClaimType = "ProfileId",
                };
            });
            return services;
        }
    }
}
