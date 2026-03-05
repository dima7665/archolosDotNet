using archolosDotNet.Database;
using archolosDotNet.Models.UserNS;

namespace archolosDotNet.Services.UserNS;

public interface IUserService
{
    public IQueryable<SimpleUser> GetAll();
    public UserDto CreateUser(UserDto data);
    public bool Delete(int id);

    public SimpleUser? GetUserByEmail(string email);
}

public class UserService(ApplicationDbContext context) : IUserService
{
    private readonly ApplicationDbContext dbContext = context;

    public IQueryable<SimpleUser> GetAll()
    {
        return dbContext.Users.Select(u => new SimpleUser(u));
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
        return dbContext.Users.Select(u => new SimpleUser(u)).SingleOrDefault(u => u.email == email);
    }
}
