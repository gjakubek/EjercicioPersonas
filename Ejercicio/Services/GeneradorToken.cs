using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System;
using Microsoft.Extensions.Configuration;

namespace Ejercicio.Services
{
    public class GeneradorToken : IGeneradorToken
    {
        public string TokenApis(double horasVigencia)
        {
            try
            {
                byte[] privateKey = Convert.FromBase64String(Startup.StaticConfig.GetValue<string>("JWT:Private"));
                using RSA rsa = RSA.Create();
                rsa.ImportRSAPrivateKey(privateKey, out _);
                var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
                {
                    CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
                };
                var now = DateTime.Now;
                var unixTimeSeconds = new DateTimeOffset(now).ToUnixTimeSeconds();
                var jwt = new JwtSecurityToken(
                        audience: Startup.StaticConfig.GetValue<string>("JWT:Audience"),
                        issuer: Startup.StaticConfig.GetValue<string>("JWT:Issuer"),
                        claims: new Claim[] {
                            new Claim(JwtRegisteredClaimNames.Iat, unixTimeSeconds.ToString(), ClaimValueTypes.Integer64),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        },
                        notBefore: now,
                        expires: now.AddHours(horasVigencia),
                        signingCredentials: signingCredentials
                    );
                var retorno = new JwtSecurityTokenHandler().WriteToken(jwt);
                return retorno;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
