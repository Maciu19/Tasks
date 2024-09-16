using System.Text.Json.Serialization;

namespace Domain.Notes;

public class Note
{
    public Guid Id { get; }
    public Guid UserId { get; }
    public List<Guid> CollaboratorsIds { get; set; } = [];
    public List<int> LabelsIds { get; set; } = [];
    public string Title { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public DateTime LastEdited { get; private set; }
    public DateTime? DueDate { get; private set; }
    public bool Fixed { get; private set; }
    public string? Background { get; private set; }
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
