using System.Text.Json.Serialization;

public class Author : IEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Biography { get; set; }

    [JsonIgnore]
    public List<Book> Books { get; set; }
}