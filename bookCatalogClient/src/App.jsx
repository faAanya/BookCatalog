import { useEffect, useState } from 'react'
import { fetchBooks } from './controllers/bookController';
import { BookCard } from './components/bookCard';
import { CreateBookForm } from './components/createBookForm';
import './App.css'

function App() {
  const [books, setData] = useState([]);
  const [showCreateForm, setShowCreateForm] = useState(false);
  useEffect(() => {
    const fetchData = async () => {
      var books = await fetchBooks();
      setData(books);
    }
    fetchData();
  }, []);

  const handleBookCreated = (newBook) => {
    setData((prev) => [...prev, newBook]);
  };

  return (
    <>
      <header>
        <h1>Book Catalog</h1>
      </header>

      {showCreateForm && (
        <CreateBookForm onBookCreated={handleBookCreated} onCancel={() => setShowCreateForm(false)} />
      )}
      {!showCreateForm &&
        <button onClick={() => setShowCreateForm(true)}>
          Add New Book
        </button>

      }
      {!showCreateForm &&
        books.map((b) => (
          <div key={b.id}>
            <BookCard
              title={b.title}
              description={b.description}
              isbn={b.isbn}
              publicationYear={b.publicationYear}
              coverImageUrl={b.coverImageUrl}
              pageCount={b.pageCount}
            />
          </div>
        ))
      }
    </>
  )
}

export default App
