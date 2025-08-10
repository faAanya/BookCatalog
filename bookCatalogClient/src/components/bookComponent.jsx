import { fetchAuthor } from "../controllers/authorController";
import { fetchGenre } from "../controllers/genreController";
import "../styles/bookCard.css"
import { BookImage } from "./imageComponent";
import { useState, useEffect } from "react";
export const Book = ({
    id,
    title,
    description,
    isbn,
    publicationYear,
    coverImageUrl,
    pageCount,
    authorsId,
    genresId,
    onClick
}) => {

    const [authors, setAuthors] = useState([])
    const [genres, setGenres] = useState([])

    useEffect(() => {
        if (!authorsId?.length) return;

        const loadAuthors = async () => {
            try {
                const results = await authorIds.map((id) => fetchAuthor(id))
                console.log(results);

                setAuthors(results);
            } catch (err) {
                console.error("Error fetching authors:", err);
            }
        };

        const loadGenres = async () => {
            try {
                const results = await genresId.map((id) => fetchGenre(id))
                console.log(results);

                setGenres(results);
            } catch (err) {
                console.error("Error fetching genres:", err);
            }
        };

        loadAuthors();
        loadGenres();
    }, [authorsId, genresId]);

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
                <strong>Authors:</strong> {authorsData.map((a) => a.firstName).join(", ")}
            </div>

            <div>
                <strong>Genres:</strong> {genres.map((g) => a.name).join(", ")}
            </div>
        </div >
    );
};