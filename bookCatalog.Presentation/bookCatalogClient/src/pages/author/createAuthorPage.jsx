import { useState, useEffect } from 'react';
import "../../styles/createBookPage.css"
import { useNavigate } from 'react-router-dom';
import { addAuthor } from '../../controllers/authorController';

export const CreateAuthorPage = () => {
    const navigate = useNavigate();

    const [form, setForm] = useState({
        firstName: '',
        lastName: '',
        biography: '',
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setForm((prev) => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const newAuthor = {
            ...form,
        };
        console.log(newAuthor);

        try {
            await addAuthor(newAuthor);
            setForm({
                firstName: '',
                lastName: '',
                biography: ''
            });

        } catch (err) {
            alert("Error adding author");
            console.error(err);
        }
    };

    const cancelAuthorCreation = () => {
        navigate("/authors");
    }


    return (
        <>
            <h2>Create Author</h2>
            <div className="create-book-form">
                <form onSubmit={handleSubmit}>
                    <input name="firstName" value={form.firstName} onChange={handleChange} placeholder="First Name" required />
                    <input name="lastName" value={form.lastName} onChange={handleChange} placeholder="Last Name" required />
                    <textarea name="biography" value={form.biography} onChange={handleChange} placeholder="Biography" />

                    <button type="submit">Add Author</button>
                    <button type="button" onClick={cancelAuthorCreation}>Cancel</button>
                </form>
            </div>
        </>
    );
};
