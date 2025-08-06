import { useEffect, useState } from 'react';
import { fetchGenres } from '../../controllers/genreController.js';
import { useNavigate } from 'react-router-dom';
import "../../styles/homePage.css"
import { Genre } from '../../components/genreComponent.jsx';

export const GenresPage = () => {
    const [genres, setGenres] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const loadGenres = async () => {
            const allGenres = await fetchGenres();
            setGenres(allGenres);
        };
        loadGenres();
    }, []);

    return (
        <>
            <button onClick={() => navigate("/")}>Back to books</button>
            <h1>Genres</h1>
            <button onClick={() => navigate("/createGenre")}>Add New Genre</button>
            <div className="book-list">
                {genres.map(genre => (
                    <Genre
                        key={genre.id}
                        name={genre.name}
                        description={genre.description}
                        onClick={() => { navigate(`/genre/${genre.id}`) }}
                    />
                ))}
            </div>
        </>
    );
};