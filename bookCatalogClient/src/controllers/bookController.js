import axios from "axios";

export const fetchBooks = async () => {
    try {
        const response = await axios.get("http://localhost:5013/books");
        return response.data;
    } catch (error) {
        console.error("Axios fetch error:", error);
        return [];
    }
};

export const fetchBook = async (id) => {
    try {
        const response = await axios.get(`http://localhost:5013/books/${id}`);
        return response.data;
    } catch (error) {
        console.error("Axios fetch error:", error);
        return null;
    }
};

export const addBook = async (book) => {
    try {
        const response = await axios.post("http://localhost:5013/books", book, {
            headers: { "Content-Type": "application/json" },
        });
        return response.data;
    } catch (error) {
        console.error("Axios addBook error:", error);
        throw error;
    }
};

export const deleteBook = async (id) => {
    try {
        await axios.delete(`http://localhost:5013/books/${id}`);
    } catch (error) {
        console.error("Axios delete book error", error);
        throw error;
    }
};

export const updateBook = async (id, updatedBook) => {
    try {
        const response = await axios.put(`http://localhost:5013/books/${id}`, updatedBook, {
            headers: { "Content-Type": "application/json" },
        });
        return response.data;
    } catch (error) {
        console.error("Axios update book error", error);
        throw error;
    }
};
