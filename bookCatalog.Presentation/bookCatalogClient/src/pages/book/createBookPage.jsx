import { useState, useEffect } from 'react';
import { addBook } from '../../controllers/bookController';
import "../../styles/createBookPage.css"
import { useNavigate } from 'react-router-dom';
import { fetchAuthors } from '../../controllers/authorController';
import { fetchGenres } from '../../controllers/genreController';
import { downloadImage } from '../../controllers/imageController';
export const CreateBookPage = () => {
    const navigate = useNavigate();
    const [authors, setAuthors] = useState([]);
    const [genres, setGenres] = useState([]);


    const [selectedAuthors, setSelectedAuthors] = useState([]);
    const [selectedGenres, setSelectedGenres] = useState([]);

    const [form, setForm] = useState({
        title: '',
        description: '',
        isbn: '',
        publicationYear: '',
        coverImageUrl: '',
        pageCount: '',
    });

    useEffect(() => {
        const loadData = async () => {
            const authors = await fetchAuthors();
            const genres = await fetchGenres();
            setAuthors(authors);
            setGenres(genres);
        };
        loadData();
    }, []);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setForm((prev) => ({ ...prev, [name]: value }));
    };

    const handleSelectAuthor = (e) => {
        const author = e.target.value;
        if (!selectedAuthors.includes(author)) {
            setSelectedAuthors([...selectedAuthors, author]);
        }
    };

    const handleSelectGenre = (e) => {
        const genre = e.target.value;
        if (!selectedGenres.includes(genre)) {
            setSelectedGenres([...selectedGenres, genre]);
        }
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const newBook = {
            ...form,
            selectedAuthors,
            selectedGenres,
            authors: selectedAuthors,
            genres: selectedGenres,
            publicationYear: parseInt(form.publicationYear),
            pageCount: parseInt(form.pageCount),
        };
        console.log(newBook);

        try {
            await addBook(newBook);
            console.log(form.coverImageUrl);
            const image = {
                fileName: form.coverImageUrl
            }
            await downloadImage(image);
            setForm({
                title: '',
                description: '',
                isbn: '',
                publicationYear: '',
                coverImageUrl: '',
                pageCount: '',
            });
            setSelectedAuthors([]);
            setSelectedGenres([]);
        } catch (err) {
            alert("Error adding book");
            console.error(err);
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

                    <label>Authors:</label>
                    <select onChange={handleSelectAuthor} defaultValue="">
                        <option value="" disabled>Select author</option>
                        {authors.map(author => (
                            <option key={author.id} value={author.id}>
                                {author.firstName} {author.lastName}
                            </option>
                        ))}
                    </select>
                    <div className="selected-items">
                        {selectedAuthors.map(id => {
                            const author = authors.find(a => a.id === id);
                            return <span key={id}>{author?.firstName} {author?.lastName}; </span>;
                        })}
                    </div>

                    <label>Genres:</label>
                    <select onChange={handleSelectGenre} defaultValue="">
                        <option value="" disabled>Select genre</option>
                        {genres.map(genre => (
                            <option key={genre.id} value={genre.id}>
                                {genre.name}
                            </option>
                        ))}
                    </select>
                    <div className="selected-items">
                        {selectedGenres.map(id => {
                            const genre = genres.find(g => g.id === id);
                            return <span key={id}>{genre?.name}; </span>;
                        })}
                    </div>
                    <button type="submit">Add Book</button>
                    <button type="button" onClick={cancelBookCreation}>Cancel</button>
                </form>
            </div>
        </>
    );
};
