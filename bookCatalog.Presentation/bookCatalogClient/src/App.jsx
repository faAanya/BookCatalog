import { BookPage } from './pages/book/bookPage';
import { HomePage } from './pages/homePage';
import { Routes, Route, } from 'react-router-dom';
import { CreateBookPage } from './pages/book/createBookPage';
import { EditBookPage } from './pages/book/editBookPage';
import { AuthorsPage } from './pages/author/authorsListPage';
import { CreateAuthorPage } from './pages/author/createAuthorPage';
import { AuthorPage } from './pages/author/authorPage';
import { EditAuthorPage } from './pages/author/editAuthorPage';
import { GenresPage } from './pages/genre/genresListPage';
import { CreateGenrePage } from './pages/genre/creteGenrePage';
import { EditGenrePage } from './pages/genre/editGenrePage';
import { GenrePage } from './pages/genre/genrePage';
import { AuthPage } from './pages/user/authPage';
function App() {
  return (
    <>
      <div className="App">

        <Routes>
          {/* ?<Route path="/" element={<AuthPage />} /> */}
          <Route path="/books" element={<HomePage />} />
          <Route path="/createBook" element={<CreateBookPage />} />
          <Route path="/book/:id" element={<BookPage />} />
          <Route path="/book/:id/edit" element={<EditBookPage />} />

          <Route path="/authors" element={<AuthorsPage />} />
          <Route path="/createAuthor" element={<CreateAuthorPage />} />
          <Route path="/author/:id" element={<AuthorPage />} />
          <Route path="/author/:id/edit" element={<EditAuthorPage />} />

          <Route path="/genres" element={<GenresPage />} />
          <Route path="/createGenre" element={<CreateGenrePage />} />
          <Route path="/genre/:id" element={<GenrePage />} />
          <Route path="/genre/:id/edit" element={<EditGenrePage />} />
        </Routes>
      </div>
    </>
  );
}

export default App;
