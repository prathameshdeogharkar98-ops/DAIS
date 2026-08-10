using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.JWTServices
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string generateToken(
            string userId,
            string email
            )
        {
            var jwtSettings = _configuration.GetSection("Jwt");

            var key = jwtSettings["Key"]!;
            var issuer = jwtSettings["Issuer"]!;
            var audience = jwtSettings["Audience"]!;

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Email, email),
            

            new Claim(
                JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()
            )
        };

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key)
            );

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public String generateRefreshToken()
        {
            var salt = "";

            for (int i = 0; i < 18; i++)
            {
                Random random = new Random();

                int randomNumber = i % 2 == 0 ? random.Next(97, 123) : random.Next(64, 91);
                char randomCharacter = (char)randomNumber;

                salt = salt + randomCharacter.ToString();
            }

            return salt;
        }
    }
}
