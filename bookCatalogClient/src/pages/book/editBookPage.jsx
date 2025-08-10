import { useEffect, useState } from 'react';
import { fetchBook, updateBook } from '../../controllers/bookController';
import { useNavigate, useParams } from 'react-router-dom';
import "../../styles/editBookPage.css"

export const EditBookPage = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const [book, setBook] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const loadBook = async () => {
            try {
                const currentBook = await fetchBook(id);
                setBook(currentBook);
            } catch (error) {
                console.error("Ошибка загрузки книги:", error);
            } finally {
                setLoading(false);
            }
        };
        loadBook();
    }, [id]);

    const handleSave = async () => {
        try {
            console.log(book.id, book);

            await updateBook(book.id, book);
            navigate(`/book/${book.id}`);
        } catch (error) {
            alert("Error", error);
        }
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setBook(prev => ({ ...prev, [name]: value }));
    };

    if (loading || !book) return <div>Loading...</div>;

    return (
        <div className="edit-book-page">
            <h2>Edit book</h2>
            <input name="title" value={book.title} onChange={handleChange} placeholder="Title" />
            <textarea name="description" value={book.description} onChange={handleChange} placeholder="Description" />
            <input name="isbn" value={book.isbn} onChange={handleChange} placeholder="ISBN" />
            <input name="publicationYear" value={book.publicationYear} type="number" onChange={handleChange} placeholder="Year" />
            <input name="coverImageUrl" value={book.coverImageUrl} onChange={handleChange} placeholder="URL" />
            <input name="pageCount" value={book.pageCount} type="number" onChange={handleChange} placeholder="Pages" />
            <button onClick={handleSave}>Save</button>
            <button onClick={() => navigate(-1)}>Cancel</button>
        </div>
    );
};
