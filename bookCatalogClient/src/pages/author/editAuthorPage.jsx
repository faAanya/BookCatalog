import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import "../../styles/editBookPage.css"
import { fetchAuthor, updateAuthor } from '../../controllers/authorController';

export const EditAuthorPage = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [author, setAuthor] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const loadAuthor = async () => {
            try {
                setLoading(true);
                const currentAuthor = await fetchAuthor(id);

                if (!currentAuthor) {
                    throw new Error("Author not found");
                }

                setAuthor(currentAuthor);
                console.log("Loaded author:", currentAuthor);
            } catch (error) {
                console.error("Failed to load author:", error);
                setError(error.message);
                navigate('/authors'); // Redirect if error
            } finally {
                setLoading(false);
            }
        };
        loadAuthor();
    }, [id, navigate]);

    const handleSave = async () => {
        try {
            await updateAuthor(author.id, author);
            navigate(`/author/${author.id}`);
        } catch (error) {
            alert("Error updating author: " + error.message);
        }
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setAuthor(prev => ({ ...prev, [name]: value }));
    };

    if (loading) return <div>Loading author...</div>;
    if (error) return <div>Error: {error}</div>;
    if (!author) return <div>Author not found</div>;

    return (
        <div className="edit-book-page">
            <h2>Edit Author</h2>
            <input
                name="firstName"
                value={author.firstName || ''}
                onChange={handleChange}
                placeholder="First Name"
            />
            <input
                name="lastName"
                value={author.lastName || ''}
                onChange={handleChange}
                placeholder="Last Name"
            />
            <textarea
                name="biography"
                value={author.biography || ''}
                onChange={handleChange}
                placeholder="Biography"
            />

            <button onClick={handleSave}>Save</button>
            <button onClick={() => navigate(-1)}>Cancel</button>
        </div>
    );
};