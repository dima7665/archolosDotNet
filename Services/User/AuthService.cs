using archolosDotNet.Database;
using archolosDotNet.Migrations;
using archolosDotNet.Models.UserNS;
using Microsoft.EntityFrameworkCore;

namespace archolosDotNet.Services.UserNS;

public interface IAuthService
{
    public bool IsAuthenticated(LoginRequest user);
    public LoginResponse? Login(LoginRequest data);
    public Tokens? RefreshTokens(string token);
}

public class AuthService(ApplicationDbContext dbContext, JwtService jwtService) : IAuthService
{
    public bool IsAuthenticated(LoginRequest data)
    {
        ArgumentNullException.ThrowIfNull(data);

        var hasher = new PasswordHasher();

        var user = dbContext.Users.SingleOrDefault(u => u.email == data.email);

        if (user == null)
        {
            return false;
        }

        return hasher.VerifyPassword(data.password, user.hash);
    }

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

        if (refreshToken.expiresOn < DateTime.UtcNow.AddDays(-1))
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

    public bool Logout()
    {
        return true;
    }
}
