using System.Text.Json.Serialization;

namespace Domain.Notes;

public class Note
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<Guid> CollaboratorsIds { get; set; } = [];
    public List<int> LabelsIds { get; set; } = [];
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime LastEdited { get; set; }
    public DateTime? DueDate { get; set; }
    public bool Fixed { get; set; }
    public string? Background { get; set; }
    public bool Deleted { get; set; }

    public Note(
        Guid userId, 
        string title, 
        string content, 
        DateTime lastEdited, 
        bool fix,
        string? background = null,
        DateTime? dueDate = null)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Title = title;
        Content = content;
        LastEdited = lastEdited;    
        DueDate = dueDate;
        Fixed = fix;
        Background = background;
        Deleted = false;
    }

    public void Update(
        string title, 
        string content,
        DateTime lastEdited,
        bool fix,
        string? background = null,
        DateTime? dueDate = null)
    {
        Title = title;
        Content = content;
        LastEdited = lastEdited;
        DueDate = dueDate;
        Fixed = fix;
        Background = background;
    }

    [JsonConstructor]
    private Note() { }
}
