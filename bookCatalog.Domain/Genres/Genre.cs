using System.Text.Json.Serialization;

public class Genre : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    [JsonIgnore]
    public List<Book> Books { get; set; }
}