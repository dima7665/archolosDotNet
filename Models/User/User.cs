using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace archolosDotNet.Models.UserNS;

public class User : SimpleUser
{
    [Key]
    public new int id { get; set; }
    public required string hash { get; set; }
}

public class UserDto : SimpleUser
{
    public new int? id { get; set; }
    public required string password { get; set; }

    public new UserRole? role { get; set; }
}

public class SimpleUser
{
    public int? id { get; set; }
    public required string email { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }

    public UserRole role { get; set; }

    public SimpleUser() { }
    
    [SetsRequiredMembers]
    public SimpleUser(User data)
    {
        id = data.id;
        email = data.email;
        firstName = data.firstName;
        lastName = data.lastName;
        role = data.role;
    }
}

public class LoginRequest
{
    public required string email { get; set; }
    public required string password { get; set; }
}

public class LoginResponse
{
    public required SimpleUser user { get; init; }
    public required Tokens tokens { get; set; }
}

public class Tokens
{
    public string accessToken { get; set; }
    public string refreshToken { get; set; }
}

public enum UserRole
{
    Super,
    Admin,
    Other
}
