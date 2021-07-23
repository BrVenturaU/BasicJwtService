using BasicJwtService.Services;
using BasicJwtService.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BasicJwtService
{
    /// <summary>
    /// Extension class for the token's services that must be call in the IoC.
    /// </summary>
    public static class JwtServiceConfiguration
    {
        /// <summary>
        /// IServiceCollection extension method that configures the token's services.
        /// </summary>
        /// <param name="services">IServiceCollection instance.</param>
        /// <param name="configuration">IConfiguration instance.</param>
        /// <param name="secret">The token's secret/private key.</param>
        /// <returns>The same IServiceCollection instance with the token's services configured.</returns>
        public static IServiceCollection ConfigureTokenServices(this IServiceCollection services, IConfiguration configuration, string secret)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ITokenService, TokenService>();
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            services.AddScoped(sp => {
                var jwtSettings = sp.GetRequiredService<IOptions<JwtSettings>>().Value;
                jwtSettings.Secret = secret;
                return jwtSettings;
            });

            var jwtSettings = new JwtSettings() { Secret = secret};
            configuration.GetSection(nameof(JwtSettings)).Bind(jwtSettings);
            
            services.ConfigureJwt(jwtSettings);
            return services;
        }

        private static IServiceCollection ConfigureJwt(this IServiceCollection services, JwtSettings jwtSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidateLifetime = jwtSettings.ValidateLifetime,
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });
            return services;
        }
    }
}
