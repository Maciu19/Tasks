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

    public Label(Guid userId, string name)
    {
        UserId = userId;
        Name = name;
    }

    [JsonConstructor]
    private Label() { }
}
