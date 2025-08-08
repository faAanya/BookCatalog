public static class BookMapper
{
    public static GetBookDTO BookToDTO(Book book)
    {
        return new GetBookDTO()
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            ISBN = book.ISBN,
            PublicationYear = book.PublicationYear,
            CoverImageUrl = book.CoverImageUrl,
            PageCount = book.PageCount,
            Authors = book.Authors.Select(a => $"{a.FirstName} {a.LastName}").ToList(),
            Genres = book.Genres.Select(g => g.Name).ToList()
        };
    }

    public static Book DTOtoBook(CreateBookDTO bookDTO)
    {
        return new Book()
        {
            Title = bookDTO.Title,
            Description = bookDTO.Description,
            ISBN = bookDTO.ISBN,
            PublicationYear = bookDTO.PublicationYear,
            CoverImageUrl = bookDTO.CoverImageUrl,
            PageCount = bookDTO.PageCount
        };
    }
}