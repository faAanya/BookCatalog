import { BookPage } from './pages/bookPage';
import { HomePage } from './pages/homePage';
import { Routes, Route, } from 'react-router-dom';
import { CreateBookPage } from './pages/createBookPage';
import { EditBookPage } from './pages/editBookPage';

function App() {
  return (
    <>
      <div className="App">
        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/createBook" element={<CreateBookPage />} />
          <Route path="/book/:id" element={<BookPage />} />
          <Route path="/book/:id/edit" element={<EditBookPage />} />
        </Routes>
      </div>
    </>
  );
}

export default App;
