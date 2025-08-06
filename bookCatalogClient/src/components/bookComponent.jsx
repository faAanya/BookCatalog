import "../styles/bookCard.css"
import { BookImage } from "./imageComponent";
export const Book = ({
    id,
    title,
    description,
    isbn,
    publicationYear,
    coverImageUrl,
    pageCount,
    authors,
    genres,
    onClick
}) => {
    return (
        <div className="book-details" onClick={() => onClick(
            title,
            description,
            isbn,
            publicationYear,
            coverImageUrl,
            pageCount)}>
            <h2 className="book-title">{title}</h2>

            <BookImage id={id} title={title} />
            <p><strong>Description:</strong> {description ?? "—"}</p>
            <p><strong>ISBN:</strong> {isbn ?? "—"}</p>
            <p><strong>Publication Year:</strong> {publicationYear ?? "—"}</p>
            <p><strong>Page Count:</strong> {pageCount ?? "—"}</p>

            <div>
                <strong>Authors:</strong> {authors.join(", ")}
            </div>

            <div>
                <strong>Genres:</strong> {genres.join(", ")}
            </div>
        </div >
    );
};