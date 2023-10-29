using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.Application.Tools.Options;
using webapi.Core.Entities;
using webapi.Core.Interfaces.Services;

namespace webapi.Application.Services
{
    internal class TokenService : ITokenService
    {
        private readonly IConfigurationSection _configuration;

        public TokenService(IConfiguration configuration, IOptions<TokenServiceOptions> options)
        {
            _configuration = configuration.GetRequiredSection(options.Value.SectionName);
        }

        string ITokenService.CreateToken(Usuario user, IEnumerable<string> roles)
        {
            var key = Encoding.Unicode.GetBytes(_configuration.GetValue<string>("SecretKey")!);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new(JwtRegisteredClaimNames.UniqueName, user.NombreUsuario));
            claims.AddClaim(new(JwtRegisteredClaimNames.NameId, user.Id.ToString()!));
            claims.AddClaim(new(JwtRegisteredClaimNames.Name, $"{user.Nombre} {user.ApellidoP} {user.ApellidoM}".TrimEnd()));

            foreach (var role in roles)
                claims.AddClaim(new(ClaimTypes.Role, role));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration.GetValue<string>("Issuer"),
                IssuedAt = DateTime.UtcNow,
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(_configuration.GetValue<double>("ExpiresInHours")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);
        }

        Task<string> ITokenService.CreateTokenAsync(Usuario user, IEnumerable<string> roles)
        {
            throw new NotImplementedException("This implementation doesn't require an asynchronous version.");
        }
    }
}
