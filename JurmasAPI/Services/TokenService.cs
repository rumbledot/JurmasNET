using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using JurmasModels;

namespace JurmasAPI.Services
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, Jurmas user);
        Jurmas GetUser(string token);
    }

    public class TokenService : ITokenService
    {
        //expired in 24 hours
        private TimeSpan ExpiryDuration = new TimeSpan(24, 0, 0);
        public string BuildToken(string key, string issuer, Jurmas user)
        {
            var claims = new[]
            {
                new Claim("activeUser", JsonConvert.SerializeObject(user))
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer, issuer, claims,
                expires: DateTime.Now.Add(ExpiryDuration),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public Jurmas GetUser(string token)
        {
            try
            {
                if (token.StartsWith("Basic"))
                {
                    Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                    string encodedUsernamePassword = token.Substring(6).Trim();
                    string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                    int seperatorIndex = usernamePassword.IndexOf(':');

                    return new Jurmas()
                    {
                        Username = usernamePassword.Substring(0, seperatorIndex),
                        Password = usernamePassword.Substring(seperatorIndex + 1)
                    };
                }
                else if (token.StartsWith("Bearer"))
                {
                    var handler = new JwtSecurityTokenHandler();

                    var activeToken = handler.ReadJwtToken(token.Replace("Bearer ", ""));

                    var userString = activeToken.Payload.First().Value;

                    return JsonConvert.DeserializeObject<Jurmas>(userString.ToString());
                }

                throw new Exception("Token Auth not recognized");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}