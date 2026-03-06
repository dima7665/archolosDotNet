using System.Security.Claims;
using archolosDotNet.Database;
using archolosDotNet.Models.UserNS;
using Microsoft.EntityFrameworkCore;

namespace archolosDotNet.Services.UserNS;

public interface IAuthService
{
    public LoginResponse? Login(LoginRequest data);
    public void Logout(string token);
    public Tokens? RefreshTokens(string token);
}

public class AuthService(ApplicationDbContext dbContext, JwtService jwtService, IHttpContextAccessor httpContextAccessor) : IAuthService
{
    public LoginResponse? Login(LoginRequest data)
    {
        var user = dbContext.Users.SingleOrDefault(u => u.email == data.email);

        if (user == null)
        {
            return null;
        }

        var hasher = new PasswordHasher();

        if (!hasher.VerifyPassword(data.password, user.hash))
        {
            return null;
        }

        var refreshToken = jwtService.GenerateRefreshToken();

        dbContext.RefreshTokens.Add(new RefreshToken
        {
            token = refreshToken,
            expiresOn = DateTime.UtcNow.AddDays(5),
            userId = user.id,
        });
        dbContext.SaveChanges();

        return new LoginResponse
        {
            user = new SimpleUser(user),
            tokens = new Tokens
            {
                accessToken = jwtService.CreateAccessToken(user),
                refreshToken = refreshToken,
            }
        };
    }

    public Tokens? RefreshTokens(string token)
    {

        RefreshToken? refreshToken = dbContext.RefreshTokens.Include(r => r.user).SingleOrDefault(r => r.token == token);

        if (refreshToken is null || refreshToken.expiresOn < DateTime.UtcNow)
        {
            return null;
        }

        if (refreshToken.userId != GetCurrentUserId())
        {
            // щоб витягти ід юзера має передаватися AccessToken, а цей виклик буде робитися тільки якщо він expired, тобто без нього

            // dbContext.RefreshTokens.Where(r => r.userId == refreshToken.userId).ExecuteDelete();
            // dbContext.SaveChanges();
            // return null;
        }

        if (refreshToken.expiresOn < DateTime.UtcNow.AddDays(-5).AddHours(3))
        {
            refreshToken.token = jwtService.GenerateRefreshToken();
            refreshToken.expiresOn = DateTime.UtcNow.AddDays(5);
            dbContext.SaveChanges();
        }

        return new Tokens
        {
            accessToken = jwtService.CreateAccessToken(refreshToken.user),
            refreshToken = refreshToken.token,
        };
    }

    public void Logout(string token)
    {
        var _token = dbContext.RefreshTokens.SingleOrDefault(r => r.token == token);

        if (_token != null)
        {
            dbContext.RefreshTokens.Remove(_token);
            dbContext.SaveChanges();
        }
    }

    private int? GetCurrentUserId()
    {
        var id = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

        return id != null ? int.Parse(id) : null;
    }
}
