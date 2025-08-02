import { useEffect, useState } from 'react';
import { fetchBooks } from './controllers/bookController';
import { BookCard } from './components/bookCard';
import { CreateBookForm } from './components/createBookForm';
import { BookPage } from './components/bookPage';
import './App.css';

function App() {
  const [books, setBooks] = useState([]);
  const [showCreateForm, setShowCreateForm] = useState(false);
  const [selectedBook, setSelectedBook] = useState(null);

  useEffect(() => {
    const loadBooks = async () => {
      const allBooks = await fetchBooks();
      setBooks(allBooks);
    };
    loadBooks();
  }, []);

  const handleBookCreated = (newBook) => {
    setBooks(prev => [...prev, newBook]);
    setShowCreateForm(false);
  };

  const handleDeleteSuccess = (deletedId) => {
    setBooks(prev => prev.filter(book => book.id !== deletedId));
    setSelectedBook(null);
  };

  const handleUpdateSuccess = (updatedBook) => {
    setBooks(prev =>
      prev.map(book => book.id === updatedBook.id ? updatedBook : book)
    );
    setSelectedBook(null);
  };

  return (
    <div className="App">
      <h1>Book Catalog</h1>

      {showCreateForm ? (
        <CreateBookForm
          onBookCreated={handleBookCreated}
          onCancel={() => setShowCreateForm(false)}
        />
      ) : (
        <>
          <button onClick={() => setShowCreateForm(true)}>Add New Book</button>
          {!selectedBook && (
            <div className="book-list">
              {books.map(book => (
                <BookCard
                  key={book.id}
                  id={book.id}
                  title={book.title}
                  description={book.description}
                  isbn={book.isbn}
                  publicationYear={book.publicationYear}
                  coverImageUrl={book.coverImageUrl}
                  pageCount={book.pageCount}
                  onClick={() => setSelectedBook(book)}
                />
              ))}
            </div>
          )}
          {selectedBook && (
            <BookPage
              book={selectedBook}
              onClose={() => setSelectedBook(null)}
              onDeleteSuccess={handleDeleteSuccess}
              onUpdateSuccess={handleUpdateSuccess}
            />
          )}
        </>
      )}
    </div>
  );
}

export default App;
