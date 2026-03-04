using archolosDotNet.EF;
using archolosDotNet.Models.User;
using archolosDotNet.Models.UserNS;

namespace archolosDotNet.Services.UserNS;

public interface IAuthService
{
    public IQueryable<SimpleUser> GetAll();
    public UserDto CreateUser(UserDto data);
    public bool Delete(int id);

    public SimpleUser? GetUserByEmail(string email);
    public bool IsAuthenticated(UserDto? user);
}

public class AuthService(ApplicationDbContext context) : IAuthService
{
    private readonly ApplicationDbContext dbContext = context;

    public IQueryable<SimpleUser> GetAll()
    {
        return dbContext.Users.Select(u => new SimpleUser
        {
            id = u.id,
            email = u.email,
            role = u.role,
            firstName = u.firstName,
            lastName = u.lastName,
        });
    }

    public UserDto CreateUser(UserDto data)
    {
        var hasher = new PasswordHasher();

        var newUser = new User
        {
            email = data.email,
            hash = hasher.HashPassword(data.password),
            firstName = data.firstName,
            lastName = data.lastName,
            role = (UserRole)(data.role.HasValue ? data.role : UserRole.Other),
        };

        dbContext.Users.Add(newUser);
        dbContext.SaveChanges();
        data.id = newUser.id;

        return data;
    }

    public bool Delete(int id)
    {
        var item = dbContext.Users.Find(id);

        if (item == null)
        {
            return false;
        }

        dbContext.Users.Remove(item);
        dbContext.SaveChanges();

        return true;
    }

    public SimpleUser? GetUserByEmail(string email)
    {
        return dbContext.Users.Select(u => new SimpleUser
        {
            id = u.id,
            email = u.email,
            role = u.role,
            firstName = u.firstName,
            lastName = u.lastName,
        }).SingleOrDefault(u => u.email == email);
    }

    public bool IsAuthenticated(UserDto? data)
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
}
