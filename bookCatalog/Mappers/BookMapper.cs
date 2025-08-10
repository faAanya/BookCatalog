public static class BookMapper
{
    public static BookDTO BookToDTO(Book book)
    {
        return new BookDTO()
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            ISBN = book.ISBN,
            PublicationYear = book.PublicationYear,
            CoverImageUrl = book.CoverImageUrl,
            PageCount = book.PageCount,
            Authors = book.Authors.Select(a => a.Id).ToList(),
            Genres = book.Genres.Select(g => g.Id).ToList()
        };
    }

    public static Book DTOtoBook(BookDTO bookDTO)
    {
        return new Book()
        {
            Id = bookDTO.Id,
            Title = bookDTO.Title,
            Description = bookDTO.Description,
            ISBN = bookDTO.ISBN,
            PublicationYear = bookDTO.PublicationYear,
            CoverImageUrl = bookDTO.CoverImageUrl,
            PageCount = bookDTO.PageCount
        };
    }
}