namespace Challenge.Services.WebApi.Modules.Feature
{
    public static class FeatureExtenstions
    {

        public static IServiceCollection AddFeature(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMvc();

            return services;

        }
    }
}
