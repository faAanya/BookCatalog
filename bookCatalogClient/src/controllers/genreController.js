import axios from "axios";

export const fetchGenres = async () => {
    try {
        const response = await axios.get("http://localhost:5013/genres");
        return response.data;
    } catch (error) {
        console.error("Axios fetch error:", error);
        return [];
    }
};

export const fetchGenre = async (id) => {
    try {
        const response = await axios.get(`http://localhost:5013/genres/${id}`);
        return response.data;
    } catch (error) {
        console.error("Axios fetch error:", error);
        return null;
    }
};

export const addGenre = async (genre) => {
    try {
        const response = await axios.post("http://localhost:5013/genres", genre, {
            headers: { "Content-Type": "application/json" },
        });
        return response.data;
    } catch (error) {
        console.error("Axios add genre error:", error);
        throw error;
    }
};

export const deleteGenre = async (id) => {
    try {
        await axios.delete(`http://localhost:5013/genres/${id}`);
    } catch (error) {
        console.error("Axios delete genre error", error);
        throw error;
    }
};

export const updateGenre = async (id, updatedGenre) => {
    try {
        const response = await axios.put(`http://localhost:5013/genres/${id}`, updatedGenre, {
            headers: { "Content-Type": "application/json" },
        });
        return response.data;
    } catch (error) {
        console.error("Axios update genre error", error);
        throw error;
    }
};
