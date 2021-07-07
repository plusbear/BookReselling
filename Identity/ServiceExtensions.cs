using Identity.Infrastructure;
using Identity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Identity
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
             var builder = services.AddIdentityCore<User>(o =>
             {
                 o.Password.RequireDigit = true;
                 o.Password.RequireLowercase = false;
                 o.Password.RequireUppercase = false;
                 o.Password.RequireNonAlphanumeric = false;
                 o.Password.RequiredLength = 10;
                 o.User.RequireUniqueEmail = true;
             });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<IdentityContext>()
                //.AddDefaultTokenProviders()
                ;
        }

        //used for auth on this microservice
        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = Environment.GetEnvironmentVariable("SECRET");
            var validIssuer = configuration.GetValue<string>("JwtSettings:ValidIssuer");
            var validAudience = configuration.GetValue<string>("JwtSettings:ValidAudience");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,

                    ValidIssuer = validIssuer,
                    ValidAudience = validAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }
    }
}
