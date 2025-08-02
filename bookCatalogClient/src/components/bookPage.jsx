import { useState } from 'react';
import { deleteBook, updateBook } from '../controllers/bookController';

export const BookPage = ({ book, onClose, onDeleteSuccess, onUpdateSuccess }) => {
    const [editing, setEditing] = useState(false);
    const [editedBook, setEditedBook] = useState({ ...book });

    const handleDelete = async () => {
        console.log(book);

        await deleteBook(book.id);
        onDeleteSuccess(book.id);
        onClose();
    };

    const handleSave = async () => {
        console.log(book);

        const updated = await updateBook(book.id, editedBook);
        onUpdateSuccess(updated);
        setEditing(false);
        onClose();
    };

    return (
        <div className="modal-overlay">
            <div className="modal-content">
                {!editing ? (
                    <>
                        <h2>{book.title}</h2>
                        <p><strong>ISBN:</strong> {book.isbn}</p>
                        <p><strong>Description:</strong> {book.description}</p>
                        <p><strong>Year:</strong> {book.publicationYear}</p>
                        <p><strong>Pages:</strong> {book.pageCount}</p>
                        <button onClick={() => setEditing(true)}>Edit</button>
                        <button onClick={handleDelete}>Delete</button>
                        <button onClick={onClose}>Закрыть</button>
                    </>
                ) : (
                    <>
                        <input value={editedBook.title} onChange={e => setEditedBook({ ...editedBook, title: e.target.value })} />
                        <textarea value={editedBook.description} onChange={e => setEditedBook({ ...editedBook, description: e.target.value })} />
                        <input value={editedBook.isbn} onChange={e => setEditedBook({ ...editedBook, isbn: e.target.value })} />
                        <button onClick={handleSave}>Submit</button>
                        <button onClick={() => setEditing(false)}>Cancel</button>
                    </>
                )}
            </div>
        </div>
    );
};
