using System.Text.Json.Serialization;

namespace Domain.Access;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public bool Deleted { get; set; }

    public User(string username, string email, string password, string displayName)
    {
        Id = Guid.NewGuid();
        Username = username;
        Email = email;
        Password = password;
        DisplayName = displayName;
        Deleted = false;
    }

    [JsonConstructor]
    private User() { }
}
