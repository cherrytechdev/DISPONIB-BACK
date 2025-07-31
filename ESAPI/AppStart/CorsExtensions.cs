using ESAPI.Constants;

namespace ESAPI.AppStart
{
    public static class CorsExtensions
    {
        internal static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            //CORS Service Configuration
            services.AddCors(options =>
            {
                var corsPolicySettings = configuration.GetSection("CORSPolicySettings");
                var allowedOrigins = corsPolicySettings.GetValue<string>("AllowedOrigins");
                var allowedMethods = corsPolicySettings.GetValue<string>("AllowedMethods");
                options.AddPolicy(name: ApiConstant.CorsPolicy,
                builder =>
                {
                    builder.WithOrigins(allowedOrigins.Split(','))
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader()
                            .WithMethods(allowedMethods.Split(','))
                            .SetIsOriginAllowed(origin => true);
                });
            });
            return services;
        }
    }
}
