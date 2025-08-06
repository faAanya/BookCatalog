import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import "../../styles/editBookPage.css"
import { fetchGenre, updateGenre } from '../../controllers/genreController';

export const EditGenrePage = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [genre, setGenre] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const loadGenre = async () => {
            try {
                setLoading(true);
                const currentGenre = await fetchGenre(id);

                if (!currentGenre) {
                    throw new Error("Genre not found");
                }

                setGenre(currentGenre);
            } catch (error) {
                console.error("Failed to load genre:", error);
                setError(error.message);
                navigate('/genres');
            } finally {
                setLoading(false);
            }
        };
        loadGenre();
    }, [id, navigate]);

    const handleSave = async () => {
        try {
            await updateGenre(genre.id, genre);
            navigate(`/genre/${genre.id}`);
        } catch (error) {
            alert("Error updating genre: " + error.message);
        }
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setGenre(prev => ({ ...prev, [name]: value }));
    };

    if (loading) return <div>Loading genre...</div>;
    if (error) return <div>Error: {error}</div>;
    if (!genre) return <div>Genre not found</div>;

    return (
        <div className="edit-book-page">
            <h2>Edit Genre</h2>
            <input
                name="name"
                value={genre.name || ''}
                onChange={handleChange}
                placeholder="Name"
            />
            <textarea
                name="description"
                value={genre.description || ''}
                onChange={handleChange}
                placeholder="Description"
            />
            <button onClick={handleSave}>Save</button>
            <button onClick={() => navigate(-1)}>Cancel</button>
        </div>
    );
};