import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import "../../styles/bookPage.css"
import { deleteAuthor, fetchAuthor } from '../../controllers/authorController';

export const AuthorPage = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [author, setAuthor] = useState(null);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        const loadAuthor = async () => {
            try {
                const currentAuthor = await fetchAuthor(id);
                setAuthor(currentAuthor);
            } catch (error) {
                console.error("Failed to load author:", error);
            } finally {
                setIsLoading(false);
            }
        };
        loadAuthor();
    }, [id, navigate]);

    const handleDelete = async () => {
        try {
            await deleteAuthor(author.id);
            navigate("/authors");
        } catch (error) {
            console.error("Failed to delete author:", error);
        }
    };

    if (isLoading) {
        return <div>Loading...</div>;
    }

    if (!author) {
        return <div>Author not found</div>;
    }

    return (
        <div className="modal-overlay">
            <div className="modal-content">
                <p><strong>First Name:</strong> {author.firstName ?? "—"}</p>
                <p><strong>Last Name:</strong> {author.lastName ?? "—"}</p>
                <p><strong>Biography</strong> {author.biography ?? "—"}</p>
                <button onClick={() => navigate(`/author/${author.id}/edit`)}>Edit</button>
                <button onClick={handleDelete}>Delete</button>
                <button onClick={() => navigate("/authors")}>Cancel</button>
            </div>
        </div>
    );
};