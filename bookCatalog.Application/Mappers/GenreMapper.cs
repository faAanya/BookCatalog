public static class GenreMapper
{
    public static GenreDTO GenreToDTO(Genre genre)
    {
        return new GenreDTO()
        {
            Id = genre.Id,
            Name = genre.Name,
            Description = genre.Description
        };
    }

    public static Genre DTOtoGenre(GenreDTO genreDTO)
    {
        return new Genre()
        {
            Name = genreDTO.Name,
            Description = genreDTO.Description,
            Books = new List<Book>()
        };
    }
}