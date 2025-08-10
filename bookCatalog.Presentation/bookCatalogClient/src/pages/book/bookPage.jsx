import { useEffect, useState } from 'react';
import { deleteBook, fetchBook } from '../../controllers/bookController';
import { useNavigate, useParams } from 'react-router-dom';
import "../../styles/bookPage.css"
import { BookImage } from '../../components/imageComponent';
export const BookPage = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const [book, setBook] = useState(null);

    useEffect(() => {
        const loadBook = async () => {
            const currentBook = await fetchBook(id);
            setBook(currentBook);
        };
        loadBook();
    }, [id]);

    const handleDelete = async () => {
        await deleteBook(book.id);
        navigate("/");
    };

    if (!book) return <div>Loading...</div>;

    return (
        <div className="modal-overlay">
            <div className="modal-content">
                <BookImage id={book.id} title={book.title} />
                <h2>{book.title}</h2>
                <p><strong>ISBN:</strong> {book.isbn}</p>
                <p><strong>Description:</strong> {book.description}</p>
                <p><strong>Year:</strong> {book.publicationYear}</p>
                <p><strong>Pages:</strong> {book.pageCount}</p>
                <button onClick={() => navigate(`/book/${book.id}/edit`)}>Edit</button>
                <button onClick={handleDelete}>Delete</button>
                <button onClick={() => navigate("/")}>Cancel</button>
            </div>
        </div>
    );
};
