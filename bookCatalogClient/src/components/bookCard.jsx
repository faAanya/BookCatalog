import "../styles/bookCard.css"

export const BookCard = ({
    title,
    description,
    isbn,
    publicationYear,
    coverImageUrl,
    pageCount,
    authors = [],
    genres = [],
}) => {
    return (
        <div className="book-details">
            <h2 className="book-title">{title}</h2>

            {coverImageUrl && (
                <img
                    className="book-cover"
                    src={coverImageUrl}
                    alt={`${title} cover`}
                />
            )}

            <p><strong>Description:</strong> {description ?? "—"}</p>
            <p><strong>ISBN:</strong> {isbn ?? "—"}</p>
            <p><strong>Publication Year:</strong> {publicationYear ?? "—"}</p>
            <p><strong>Page Count:</strong> {pageCount ?? "—"}</p>

            <div>
                <strong>Authors:</strong>
                <ul>
                    {authors.length > 0
                        ? authors.map((author, index) => (
                            <li key={index}>{author.name}</li>
                        ))
                        : <li>—</li>}
                </ul>
            </div>

            <div>
                <strong>Genres:</strong>
                <ul>
                    {genres.length > 0
                        ? genres.map((genre, index) => (
                            <li key={index}>{genre.name}</li>
                        ))
                        : <li>—</li>}
                </ul>
            </div>
        </div>
    );
};