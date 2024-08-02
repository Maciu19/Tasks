using System.Text.Json.Serialization;

namespace Domain.Notes;

public class Label
{
    private int _id;
    public int Id 
    {
        get { return _id; }
        set
        {
            if (_id != default)
            {
                throw new InvalidOperationException("Id has already been initialized and cannot be changed.");
            }

            _id = value;
        }
    }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;

    private readonly Dictionary<Guid, bool> _noteIds = [];
    public IReadOnlyDictionary<Guid, bool> NoteIds 
    {
        get
        {
            return _noteIds;
        }
    }   

    public Label(Guid userId, string name)
    {
        UserId = userId;
        Name = name;
    }

    public void AddNoteId(Guid noteId, bool fix)
    {
        if (NoteIds.ContainsKey(noteId))
        {
            throw new InvalidOperationException($"Note with id {noteId} already exists in label");
        }

        _noteIds.Add(noteId, fix);
    }

    [JsonConstructor]
    private Label() { }
}
