using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagement.WebUI.JsonWebTokenAuthentication
{
    public class JsonWebTokenHandler
    {
        private readonly IConfiguration _configuration;

        public JsonWebTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JsonWebToken CreateJsonWebToken(IList<string> roles, string login, int id, string securityStamp)
        {
            var jSonWebToken = new JsonWebToken
            {
                AccessToken = CreateAccessToken(roles, login, id, securityStamp),
                RefreshToken = CreateRefreshToken(id),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["expires:expiryMinutesAccessToken"]))
            };

            return jSonWebToken;
        }

        private string CreateAccessToken(IList<string> roles, string login, int id, string securityStamp)
        {
            var claims = roles.Select(x => new Claim(x, "")).ToList();
            claims.Add(new Claim("login", login));
            claims.Add(new Claim("securityStamp", securityStamp));
            claims.Add(new Claim("userId", id.ToString()));

            var token = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["expires:expiryMinutesAccessToken"])),
                signingCredentials: GetCredentials());

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string CreateRefreshToken(int userId)
        {
            var claims = new List<Claim>
            {
                new Claim("userId", userId.ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["expires:expiryMinutesRefreshToken"])),
                signingCredentials: GetCredentials());

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private SigningCredentials GetCredentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["securityKey"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return signingCredentials;
        }

        public string GetUserClaimByRefreshToken(string token, string claimName)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            if (jwtSecurityTokenHandler.ReadToken(token) is JwtSecurityToken jwtSecurityToken)
            {
                var userIdClaim = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == claimName);

                if (userIdClaim != null) return userIdClaim.Value;
            }

            throw new InvalidOperationException("This refresh token is not valid");
        }

        public bool IsValidLifeTimeRefreshToken(string token)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            if (!(jwtSecurityTokenHandler.ReadToken(token) is JwtSecurityToken jwtSecurityToken)) return false;

            var expires = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type == "exp")?.Value;

            if (expires == null) return false;

            var expDateTime = (new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).AddSeconds(double.Parse(expires))
                .ToUniversalTime();

            return expDateTime.CompareTo(DateTime.UtcNow) >= 0;
        }
    }
}
