import { Book } from '../components/bookComponent';
import { useEffect, useState } from 'react';
import { fetchBooks } from '../controllers/bookController';
import { useNavigate } from 'react-router-dom';
import "../styles/homePage.css"

export const HomePage = () => {
    const [books, setBooks] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const loadBooks = async () => {
            const allBooks = await fetchBooks();
            console.log(allBooks);

            setBooks(allBooks);
        };
        loadBooks();
    }, []);
    return (
        <>
            <header>
                <h1>Book Catalog</h1>
                <button onClick={() => navigate("/createBook")}>Add New Book</button>
                <button onClick={() => navigate("/authors")}>Authors</button>
                <button onClick={() => navigate("/genres")}>Genres</button></header>

            <div className="book-list">
                {
                    books.map(book => (
                        <Book
                            key={book.id}
                            id={book.id}
                            title={book.title}
                            description={book.description}
                            isbn={book.isbn}
                            publicationYear={book.publicationYear}
                            coverImageUrl={book.coverImageUrl}
                            authorsId={book.authors}
                            genresId={book.genres}
                            pageCount={book.pageCount}
                            onClick={() => { navigate(`/book/${book.id}`) }}
                        />
                    ))}
            </div>
        </>
    )
}