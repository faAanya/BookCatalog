import { useState } from 'react';
import { addBook } from '../controllers/bookController';
import "../styles/createBookForm.css"
export const CreateBookForm = ({ onBookCreated, onCancel }) => {
    const [form, setForm] = useState({
        title: '',
        description: '',
        isbn: '',
        publicationYear: '',
        coverImageUrl: '',
        pageCount: '',
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setForm((prev) => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const newBook = {
            ...form,
            authors: [],
            genres: [],
            publicationYear: parseInt(form.publicationYear),
            pageCount: parseInt(form.pageCount),
        };
        try {
            const created = await addBook(newBook);
            onBookCreated(created);
            setForm();
        } catch (err) {
            alert("Error adding book");
        }
    };

    return (
        <div className="create-book-form">
            <h2>Create New Book</h2>
            <form onSubmit={handleSubmit}>
                <input name="title" value={form.title} onChange={handleChange} placeholder="Title" required />
                <textarea name="description" value={form.description} onChange={handleChange} placeholder="Description" />
                <input name="isbn" value={form.isbn} onChange={handleChange} placeholder="ISBN" />
                <input name="publicationYear" value={form.publicationYear} onChange={handleChange} placeholder="Year" type="number" />
                <input name="coverImageUrl" value={form.coverImageUrl} onChange={handleChange} placeholder="Cover URL" />
                <input name="pageCount" value={form.pageCount} onChange={handleChange} placeholder="Pages" type="number" />

                <button type="submit">Add Book</button>
                <button type="button" onClick={onCancel}>Cancel</button>
            </form>
        </div>
    );
};
