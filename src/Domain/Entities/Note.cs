using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Note
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool Deleted { get; set; }
    public bool Fixed { get; set; }
    public DateTime? DueDate { get; set; }

    public Note(Guid userId, string title, string content, DateTime? dueDate = null)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Title = title;
        Content = content;
        Deleted = false;
        Fixed = false;
        DueDate = dueDate;
    }

    public void Update(string title, string content, bool fix, DateTime? dueDate)
    {
        Title = title;
        Content = content;
        Fixed = fix;
        DueDate = dueDate;
    }

    [JsonConstructor]
    private Note() { }
}
