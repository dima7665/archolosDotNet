using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using archolosDotNet.Models.UserNS;
using Microsoft.IdentityModel.Tokens;

namespace archolosDotNet.Services.UserNS;

public class JwtService(IConfiguration configuration)
{
    public string CreateAccessToken(User user)
    {
        var key = configuration["JWT:AccessKey"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        List<Claim> claims = [
            new Claim(ClaimTypes.Email, user.email),
              new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
              new Claim(ClaimTypes.Role, user.role.ToString()),
        ];

        // List<string> roleNames = dbContext.UserRoles.Where(Uri => Uri.userId == user.id).Select(Uri => Uri.role.name).ToList();
        // claims.AddRange(roleNames.Select(r => new Claim(ClaimTypes.Role, r)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(3),
            Issuer = configuration["JWT:Issuer"],
            Audience = configuration["JWT:Audience"],
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}
