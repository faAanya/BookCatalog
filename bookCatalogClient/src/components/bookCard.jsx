import "../styles/bookCard.css"
import axios from "axios";

export const BookCard = ({
    id,
    title,
    description,
    isbn,
    publicationYear,
    coverImageUrl,
    pageCount,
    authors = [],
    genres = [],
    onClick
}) => {
    return (
        <div className="book-details" onClick={() => onClick(
            id,
            title,
            description,
            isbn,
            publicationYear,
            coverImageUrl,
            pageCount)}>
            <h2 className="book-title">{title}</h2>

            {coverImageUrl && (
                <img
                    className="book-cover"
                    src={coverImageUrl}
                    alt={`${title} cover`}
                />
            )}

            <p><strong>Id:</strong> {id ?? "—"}</p>
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