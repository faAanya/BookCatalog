import axios from "axios";

export const fetchAuthors = async () => {
    try {
        const response = await axios.get("http://localhost:3000/authors");
        return response.data;
    } catch (error) {
        console.error("Axios fetch error:", error);
        return [];
    }
};

export const fetchAuthor = async (id) => {
    try {
        const response = await axios.get(`http://localhost:3000/authors/${id}`);
        return response.data;
    } catch (error) {
        console.error("Axios fetch error:", error);
        return null;
    }
};

export const addAuthor = async (author) => {
    try {
        const response = await axios.post("http://localhost:3000/authors", author, {
            headers: { "Content-Type": "application/json" },
        });
        return response.data;
    } catch (error) {
        console.error("Axios addAuthor error:", error);
        throw error;
    }
};

export const deleteAuthor = async (id) => {
    try {
        await axios.delete(`http://localhost:3000/authors/${id}`);
    } catch (error) {
        console.error("Axios delete author error", error);
        throw error;
    }
};

export const updateAuthor = async (id, updatedAuthor) => {
    try {
        const response = await axios.put(`http://localhost:3000/authors/${id}`, updatedAuthor, {
            headers: { "Content-Type": "application/json" },
        });
        return response.data;
    } catch (error) {
        console.error("Axios update author error", error);
        throw error;
    }
};
