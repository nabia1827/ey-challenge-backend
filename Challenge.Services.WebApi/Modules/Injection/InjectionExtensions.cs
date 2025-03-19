using Challenge.Application.Interface;
using Challenge.Application.Main;
using Challenge.Domain.Core;
using Challenge.Domain.Interface;
using Challenge.Infrastructure.Data;
using Challenge.Infrastructure.Interface;
using Challenge.Infrastructure.Repository;
using Challenge.Services.WebApi.Helpers;
using Challenge.Transversal.Common;
using Challenge.Transversal.Logging;
using MailKit;

namespace Challenge.Services.WebApi.Modules.Injection
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<DapperContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.AddScoped<IAuthApplication, AuthApplication>();
            services.AddScoped<IAuthDomain, AuthDomain>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            

            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            //services.AddSingleton<IMailService, Services.MailService>();


            return services;


        }
    }
}
