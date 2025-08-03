import { BookCard } from '../components/bookCard';
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
            setBooks(allBooks);
        };
        loadBooks();
    }, []);
    return (
        <>
            <h1>Book Catalog</h1>
            <button onClick={() => navigate("/createBook")}>Add New Book</button>
            <div className="book-list">
                {books.map(book => (
                    <BookCard
                        key={book.id}
                        id={book.id}
                        title={book.title}
                        description={book.description}
                        isbn={book.isbn}
                        publicationYear={book.publicationYear}
                        coverImageUrl={book.coverImageUrl}
                        pageCount={book.pageCount}
                        onClick={() => { navigate(`/book/${book.id}`) }}
                    />
                ))}
            </div>
        </>
    )
}