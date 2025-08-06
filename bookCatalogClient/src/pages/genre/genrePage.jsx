import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import "../../styles/bookPage.css"
import { deleteGenre, fetchGenre } from '../../controllers/genreController';

export const GenrePage = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [genre, setGenre] = useState(null);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const loadGenre = async () => {
            try {
                const currentGenre = await fetchGenre(id);
                setGenre(currentGenre);
            } catch (error) {
                console.error("Failed to load genre:", error);
            } finally {
                setIsLoading(false);
            }
        };
        loadGenre();
    }, [id, navigate]);

    const handleDelete = async () => {
        try {
            await deleteGenre(genre.id);
            navigate("/genres");
        } catch (error) {
            console.error("Failed to delete genre:", error);
        }
    };

    if (isLoading) {
        return <div>Loading...</div>;
    }

    if (!genre) {
        return <div>Genre not found</div>;
    }

    return (
        <div className="modal-overlay">
            <div className="modal-content">
                <p><strong>Name:</strong> {genre.name ?? "—"}</p>
                <p><strong>Description:</strong> {genre.description ?? "—"}</p>
                <button onClick={() => navigate(`/genre/${genre.id}/edit`)}>Edit</button>
                <button onClick={handleDelete}>Delete</button>
                <button onClick={() => navigate("/genres")}>Cancel</button>
            </div>
        </div>
    );
};