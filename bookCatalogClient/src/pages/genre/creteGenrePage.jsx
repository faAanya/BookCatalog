import { useState } from 'react';
import "../../styles/createBookPage.css"
import { useNavigate } from 'react-router-dom';
import { addGenre } from '../../controllers/genreController';

export const CreateGenrePage = () => {
    const navigate = useNavigate();

    const [form, setForm] = useState({
        name: '',
        description: '',
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setForm((prev) => ({ ...prev, [name]: value }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const newGenre = {
            ...form,
        };

        try {
            await addGenre(newGenre);
            setForm({
                name: '',
                description: ''
            });
            navigate("/genres");
        } catch (err) {
            alert("Error adding genre");
            console.error(err);
        }
    };

    const cancelGenreCreation = () => {
        navigate("/genres");
    };

    return (
        <>
            <h2>Create Genre</h2>
            <div className="create-book-form">
                <form onSubmit={handleSubmit}>
                    <input name="name" value={form.name} onChange={handleChange} placeholder="Name" required />
                    <textarea name="description" value={form.description} onChange={handleChange} placeholder="Description" />
                    <button type="submit">Add Genre</button>
                    <button type="button" onClick={cancelGenreCreation}>Cancel</button>
                </form>
            </div>
        </>
    );
};