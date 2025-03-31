using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly string _key = "characteristicrepresentativeidentificationconstitutionalinfrastructuresuperintendent";

        public string Login(string username, string password)
        {
            // Simulação de banco de dados - substitua pela sua lógica real
            var users = new List<User>
            {
                new User { Id = 1, Username = username, Password = password }
            };

            var user = users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
                return null; 

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
