using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using JurmasModels;
using JurmasDAL;
using JurmasAPI.Stores;
using static JurmasAPI.WebAppExtension;

namespace JurmasAPI.Services
{
    public interface ITokenService
    {
        string BuildToken(User user);
        Jurmas GetUser(string token);
    }

    public class TokenService : ITokenService
    {
        //expired in 24 hours
        private TimeSpan ExpiryDuration = new TimeSpan(24, 0, 0);
        public string BuildToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, JsonConvert.SerializeObject(user)),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim("loginDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
            };

            var key = APIConfig.JWT_KEY;
            var issuer = APIConfig.JWT_ISSUER;

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
                var handler = new JwtSecurityTokenHandler();

                var activeToken = handler.ReadJwtToken(token.Replace("Bearer ", ""));

                var userString = activeToken.Payload.First().Value;

                var user = JsonConvert.DeserializeObject<User>(userString.ToString());

                var db = APIHost.Services.GetService<JurmasContext>();

                return db.Jurmases.FirstOrDefault(j => j.Username.Equals(user.Username));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}