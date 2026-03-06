namespace archolosDotNet.Models.UserNS;

public static class UserRoles
{
    public const string Super = "Super";
    public const string Admin = "Super, Admin";

    // Not working
    public static string Get(UserRole[] roles)
    {
        if (roles.Length == 1)
        {
            return roles[0] == UserRole.Super ? "Super"
                : roles[0] == UserRole.Admin ? "Super, Admin"
                : "Other";
        }

        return roles.Aggregate(new System.Text.StringBuilder(), (acc, cur) => acc.Append(cur + ", "), acc => acc.ToString().TrimEnd(',', ' '));
    }
}
