using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;

namespace JWTValidation
{
    public static class JWTAddExtension
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IWebHostEnvironment environment, IConfigurationBuilder builder)
        {
            IConfigurationRoot _config = builder.Build();
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var publicKey = Convert.FromBase64String(_config["JWT:Public"]);
                    RSA rsa = RSA.Create();
                    rsa.ImportSubjectPublicKeyInfo(publicKey, out _);
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _config["JWT:Issuer"],
                        ValidAudience = _config["JWT:Audience"],
                        IssuerSigningKey = new RsaSecurityKey(rsa)
                    };
                });
        }
    }
}
