using System;
using System.ComponentModel.DataAnnotations;

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
}

public enum UserRole
{
    Super,
    Admin,
    Other
}
