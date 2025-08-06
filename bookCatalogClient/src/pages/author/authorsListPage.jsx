import { useEffect, useState } from 'react';
import { fetchAuthors } from '../../controllers/authorController';
import { useNavigate } from 'react-router-dom';
import "../../styles/homePage.css"
import { Author } from '../../components/authotComponent';

export const AuthorsPage = () => {
    const [authors, setAutors] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const loadAuthors = async () => {
            const allBooks = await fetchAuthors();
            setAutors(allBooks);
        };
        loadAuthors();
    }, []);
    return (
        <>
            <button onClick={() => navigate("/")}>Back to books</button>

            <h1>Authors</h1>
            <button onClick={() => navigate("/createAuthor")}>Add New Author</button>
            <div className="book-list">
                {
                    authors.map(author => (
                        <Author
                            key={author.id}
                            firstName={author.firstName}
                            lastName={author.lastName}
                            biography={author.biography}
                            onClick={() => { navigate(`/author/${author.id}`) }}
                        />
                    ))}
            </div>
        </>
    )
}