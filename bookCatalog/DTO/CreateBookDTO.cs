public class CreateBookDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public int PublicationYear { get; set; }
    public string CoverImageUrl { get; set; }
    public int PageCount { get; set; }
    public List<Guid> Authors { get; set; } = new();
    public List<Guid> Genres { get; set; } = new();
}