import { useState } from 'react';
import { addBook } from '../controllers/bookController';
import "../styles/createBookPage.css"
import { useNavigate } from 'react-router-dom';

export const CreateBookPage = ({ onCancel }) => {
    const navigate = useNavigate();
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
            await addBook(newBook);
            setForm({
                title: '',
                description: '',
                isbn: '',
                publicationYear: '',
                coverImageUrl: '',
                pageCount: '',
            });
        } catch (err) {
            alert("Error adding book");
        }
    };

    const cancelBookCreation = () => {
        navigate("/");
    }


    return (
        <>
            <h2>Create New Book</h2>
            <div className="create-book-form">
                <form onSubmit={handleSubmit}>
                    <input name="title" value={form.title} onChange={handleChange} placeholder="Title" required />
                    <textarea name="description" value={form.description} onChange={handleChange} placeholder="Description" />
                    <input name="isbn" value={form.isbn} onChange={handleChange} placeholder="ISBN" />
                    <input name="publicationYear" value={form.publicationYear} onChange={handleChange} placeholder="Year" type="number" />
                    <input name="coverImageUrl" value={form.coverImageUrl} onChange={handleChange} placeholder="Cover URL" />
                    <input name="pageCount" value={form.pageCount} onChange={handleChange} placeholder="Pages" type="number" />
                    <button type="submit">Add Book</button>
                    <button type="button" onClick={cancelBookCreation}>Cancel</button>
                </form>
            </div>
        </>
    );
};
