namespace archolosDotNet.Models.UserNS;

public class RefreshToken
{
    public int id { get; set; }
    public string token { get; set; }
    public int userId { get; set; }
    public DateTime expiresOn { get; set; }

    public User user { get; set; }
}
