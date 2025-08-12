
public class Book : IEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public int PublicationYear { get; set; }
    public List<Author> Authors { get; set; } = new();
    public List<Genre> Genres { get; set; } = new();
    public string CoverImageUrl { get; set; }
    public int PageCount { get; set; }
}