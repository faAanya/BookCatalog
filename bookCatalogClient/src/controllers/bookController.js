import axios from "axios";

export const fetchBooks = async () => {
    try {
        const response = await axios.get("http://localhost:5013/books");
        console.log("Fetched books:", response.data);
        return response.data;
    } catch (error) {
        console.error("Axios fetch error:", error);
        return [];
    }
};