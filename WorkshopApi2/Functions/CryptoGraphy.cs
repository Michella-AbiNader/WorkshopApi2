using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Web.Http.Controllers;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;

namespace WorkshopApi2.Functions
{
    public class CryptoGraphy
    {
        private const string secretKey = "michella";
        private static readonly SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        public string generateJwtToken(string id, string email, string role)
        {
            var claims = new[] {
                new Claim("userId", id),
                new Claim("Email", email),
                new Claim("type", role)

            };
            var token = new JwtSecurityToken
                (
                    claims: claims,
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                );
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwt = tokenHandler.WriteToken(token);
            return jwt;

        }

        public bool isTokenValid(string token, ref HttpActionContext actionContext)
        {
            SecurityToken validatedToken;
            ClaimsPrincipal claimsPrincipal;
            if(token == "")
            {
                return false;
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    RequireAudience = false
                };
                claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
            }
            catch (SecurityTokenSignatureKeyNotFoundException)
            {
                return false;
            }
            int userId = int.Parse(claimsPrincipal.FindFirst("userId").Value);
            int userRole = int.Parse(claimsPrincipal.FindFirst("token").Value);

            actionContext.Request.Properties["userId"] = userId;
            actionContext.Request.Properties["type"] = userRole;
            return true;
        }

        public string encryptedPassword(string password)
        {
            using(SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                StringBuilder builder = new StringBuilder();
                for(int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public (bool verified, string token) VerifyUser(String email, string password)
        {
            SqlOperations sql = new SqlOperations();
            string query = "SELECT TOP(1) token FROM user WHERE email = @email AND password = @password";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter ("@email", email),
                new SqlParameter ("@password", encryptedPassword(password))
            };
            DataTable tokenDt = sql.sqlToDataTable(query, parameters);
            if (tokenDt != null && tokenDt.Rows.Count > 0)
            {
            string token = tokenDt.Rows[0]["token"].ToString();
                return (true, token);
            }
            return (false, "");
        }
    }
}