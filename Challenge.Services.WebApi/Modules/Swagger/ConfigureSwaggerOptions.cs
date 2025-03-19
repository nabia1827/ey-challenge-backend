using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Challenge.Services.WebApi.Modules.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(),
                Title = "SIE APP API",
                Description = "Our ASP.NET Core Web API.",
                TermsOfService = new Uri("https://www.gob.pe/mininter"),
                License = new OpenApiLicense
                {
                    Name = "--",
                    Url = new Uri("https://www.gob.pe/mininter")
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += "This version of the API has been deprecated.";
            }

            return info;
        }
    }
}
