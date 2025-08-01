import { useEffect, useState } from 'react'
import { fetchBooks } from './controllers/bookController';
import { BookCard } from './components/bookCard';
import './App.css'

function App() {
  const [books, setData] = useState([]);
  useEffect(() => {
    const fetchData = async () => {
      var books = await fetchBooks();
      setData(books);
    }
    fetchData();
  }, []);

  return (
    <>

      <h1>Book Catalog</h1>
      {
        books.map((b) => (
          <div key={b.id}>
            <BookCard title={b.title}
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
