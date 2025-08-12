import { BookPage } from './pages/book/bookPage';
import { HomePage } from './pages/homePage';
import { Routes, Route } from 'react-router-dom';
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
import { Navigate } from 'react-router-dom';

// PrivateRoute для проверки авторизации
function PrivateRoute({ children }) {
  const token = localStorage.getItem('token');
  if (!token) {
    return <Navigate to="/" replace />;
  }
  return children;
}

function App() {
  return (
    <div className="App">
      <Routes>
        {/* Страница авторизации */}
        <Route path="/" element={<AuthPage />} />

        {/* Книги */}
        <Route
          path="/books"
          element={
            <PrivateRoute>
              <HomePage />
            </PrivateRoute>
          }
        />
        <Route
          path="/createBook"
          element={
            <PrivateRoute>
              <CreateBookPage />
            </PrivateRoute>
          }
        />
        <Route
          path="/book/:id"
          element={
            <PrivateRoute>
              <BookPage />
            </PrivateRoute>
          }
        />
        <Route
          path="/book/:id/edit"
          element={
            <PrivateRoute>
              <EditBookPage />
            </PrivateRoute>
          }
        />

        {/* Авторы */}
        <Route
          path="/authors"
          element={
            <PrivateRoute>
              <AuthorsPage />
            </PrivateRoute>
          }
        />
        <Route
          path="/createAuthor"
          element={
            <PrivateRoute>
              <CreateAuthorPage />
            </PrivateRoute>
          }
        />
        <Route
          path="/author/:id"
          element={
            <PrivateRoute>
              <AuthorPage />
            </PrivateRoute>
          }
        />
        <Route
          path="/author/:id/edit"
          element={
            <PrivateRoute>
              <EditAuthorPage />
            </PrivateRoute>
          }
        />

        {/* Жанры */}
        <Route
          path="/genres"
          element={
            <PrivateRoute>
              <GenresPage />
            </PrivateRoute>
          }
        />
        <Route
          path="/createGenre"
          element={
            <PrivateRoute>
              <CreateGenrePage />
            </PrivateRoute>
          }
        />
        <Route
          path="/genre/:id"
          element={
            <PrivateRoute>
              <GenrePage />
            </PrivateRoute>
          }
        />
        <Route
          path="/genre/:id/edit"
          element={
            <PrivateRoute>
              <EditGenrePage />
            </PrivateRoute>
          }
        />
      </Routes>
    </div>
  );
}

export default App;
