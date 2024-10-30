using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTrackerAPI.Services
{

    public interface ITokenService
    {
        string GenerarToken(int usuarioId, string nombre);
    }

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerarToken(int usuarioId, string nombre)
        {
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, usuarioId.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, nombre)
                };

                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:DurationMinutes"])),
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);

            } catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
