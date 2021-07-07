using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Gateway
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration config)
        {
            var authenticationProviderKey = "MyAuthKey";

            var issuer = config.GetValue<string>("JwtSettings:ValidIssuer");
            var audience = config.GetValue<string>("JwtSettings:ValidAudience");

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET")));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = issuer,
                ValidateAudience = true,
                ValidAudience = audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = authenticationProviderKey;
            })
            .AddJwtBearer(authenticationProviderKey, x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = tokenValidationParameters;
            });

            return services;
        }
    }
}
