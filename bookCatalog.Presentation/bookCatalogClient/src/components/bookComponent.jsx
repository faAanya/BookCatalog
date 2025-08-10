import { fetchAuthor } from "../controllers/authorController";
import { fetchGenre } from "../controllers/genreController";
import "../styles/bookCard.css";
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
    const [authors, setAuthors] = useState([]);
    const [genres, setGenres] = useState([]);

    useEffect(() => {
        if (authorsId?.length) {
            const loadAuthors = async () => {
                try {
                    const results = await Promise.all(
                        authorsId.map((aid) => fetchAuthor(aid))
                    );
                    setAuthors(results.filter(Boolean)); // remove nulls if fetch failed
                } catch (err) {
                    console.error("Error fetching authors:", err);
                }
            };
            loadAuthors();
        }

        if (genresId?.length) {
            const loadGenres = async () => {
                try {
                    const results = await Promise.all(
                        genresId.map((gid) => fetchGenre(gid))
                    );
                    setGenres(results.filter(Boolean));
                } catch (err) {
                    console.error("Error fetching genres:", err);
                }
            };
            loadGenres();
        }
    }, [authorsId, genresId]);

    return (
        <div
            className="book-details"
            onClick={() =>
                onClick(
                    title,
                    description,
                    isbn,
                    publicationYear,
                    coverImageUrl,
                    pageCount
                )
            }
        >
            <h2 className="book-title">{title}</h2>

            <BookImage id={id} title={title} />
            <p><strong>Description:</strong> {description ?? "—"}</p>
            <p><strong>ISBN:</strong> {isbn ?? "—"}</p>
            <p><strong>Publication Year:</strong> {publicationYear ?? "—"}</p>
            <p><strong>Page Count:</strong> {pageCount ?? "—"}</p>

            <div>
                <strong>Authors:</strong>{" "}
                {authors.map((a) => `${a.firstName} ${a.lastName}`).join(", ")}
            </div>

            <div>
                <strong>Genres:</strong>{" "}
                {genres.map((g) => g.name).join(", ")}
            </div>
        </div>
    );
};
