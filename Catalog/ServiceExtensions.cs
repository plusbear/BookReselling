using Amazon;
using Amazon.S3;
using Catalog.Services.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var jwtconfig = config.GetSection("JwtSettings");
            var issuer = jwtconfig.GetSection("ValidIssuer").Value;
            var audience = jwtconfig.GetSection("ValidAudience").Value;

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
                o.DefaultAuthenticateScheme = "MyAuthKey";
            })
            .AddJwtBearer("MyAuthKey", x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = tokenValidationParameters;
            });

            return services;
        }

        public static IServiceCollection ConfigureAmazonS3(this IServiceCollection services)
        {
            services.AddSingleton<IAmazonS3>(p =>
            {
                var config = new AmazonS3Config
                {
                    RegionEndpoint = RegionEndpoint.EUCentral1
                };
                if (p.GetService<IHostEnvironment>().IsDevelopment())
                {
                    config.ForcePathStyle = true;
                    config.ServiceURL = "http://localhost:4566";
                }
                return new AmazonS3Client("keyid", "accesskey", config);
            });

            return services;
        }

        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IImageRepository, ImageRepository>();
            return services;
        }
    }
}
