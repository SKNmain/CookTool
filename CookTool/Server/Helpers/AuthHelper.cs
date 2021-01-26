using CookTool.Server.Repositories;
using CookTool.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CookTool.Server.Helpers
{
    public class AuthHelper
    {
        private static BlackListedTokensRepository repository = new BlackListedTokensRepository();

        public static bool IsPasswordCorrect(string inputPassword, string userPassword)
        {
            return inputPassword.Equals(CryptHelper.Decrypt(userPassword));
        }

        public static string GenerateToken(Claim[] claims, IConfiguration _configuration)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static void CheckTokenBlackListed(HttpRequest request)
        {
            var token = ExtractToken(request);
            if(!String.IsNullOrEmpty(token)) if (repository.GetAllRecords().Any(t => t.Token.Equals(token))) throw new SecurityTokenInvalidLifetimeException();
        }

        private static string ExtractToken(HttpRequest request)
        {
            StringValues value;
            request.Headers.TryGetValue("Authorization", out value);
            return value.ToArray().GetValue(0).ToString().Split(' ').GetValue(1).ToString();
        }
    }
}
