using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Eshop_RazorPage.Infrastructure.JwtUtil
{
    public static class JwtAuthenticationConfig
    {
        public static void JwtConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey
                               = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:SignInKey"])),
                    ValidAudience = configuration["JwtConfig:Audience"],
                    ValidIssuer = configuration["JwtConfig:Issuer"],
                    ValidateLifetime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                };
            });
        }
    }
}
