using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using archolosDotNet.Models.UserNS;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace archolosDotNet.Services.UserNS;

// public interface IJwtService
// {
//     public string GenerateToken(UserDto? user);
// }

// public class JwtService : IJwtService
// {
//     private readonly JwtOptions _options;

//     public JwtService(IOptions<JwtOptions> options)
//     {
//         _options = options.Value ?? throw new ArgumentNullException(nameof(options));
//     }

//     public string GenerateToken(UserDto? user)
//     {
//         ArgumentNullException.ThrowIfNull(user);

//         var key = Encoding.ASCII.GetBytes(_options.AccessKey);
//         var securityKey = new SymmetricSecurityKey(key);

//         var tokenDescriptor = new SecurityTokenDescriptor
//         {
//             Subject = new ClaimsIdentity(new[]
//           {
//               new Claim(ClaimTypes.Email, user.email),
//               new Claim(ClaimTypes.NameIdentifier, user.id?.ToString()),
//               new Claim(ClaimTypes.Role, user.role.ToString()),
//           }),
//             Expires = DateTime.UtcNow.AddMinutes(1),
//             Issuer = _options.Issuer,
//             Audience = _options.Audience,
//             SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
//         };

//         var tokenHandler = new JwtSecurityTokenHandler();
//         var token = tokenHandler.CreateToken(tokenDescriptor);

//         return tokenHandler.WriteToken(token);
//     }
// }

public class TokenProvider(IConfiguration configuration)
{
    public string Create(UserDto user)
    {
        var key = configuration["JWT:AccessKey"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
              new Claim(ClaimTypes.Email, user.email),
              new Claim(ClaimTypes.NameIdentifier, user.id?.ToString()),
              new Claim(ClaimTypes.Role, user.role.ToString()),
          ]),
            Expires = DateTime.UtcNow.AddMinutes(3),
            Issuer = configuration["JWT:Issuer"],
            Audience = configuration["JWT:Audience"],
            SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
