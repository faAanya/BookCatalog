public static class AuthorMapper
{
    public static AuthorDTO AuthorToDTO(Author author)
    {
        return new AuthorDTO()
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            Biography = author.Biography
        };
    }

    public static Author DTOtoAuthor(AuthorDTO authorDTO)
    {
        return new Author()
        {
            Id = authorDTO.Id,
            FirstName = authorDTO.FirstName,
            LastName = authorDTO.LastName,
            Biography = authorDTO.Biography,
            Books = new List<Book>()
        };
    }
}
