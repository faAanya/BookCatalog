import axios from "axios";

export const getImage = async (id) => {
    try {
        const response = await axios.get(`http://localhost:3000/images/${id}`, {
            responseType: "blob",
        });
        return response.data;
    } catch (error) {
        console.error("Error fetching image:", error);
        return null;
    }
};

export const downloadImage = async (fileName) => {
    try {
        console.log(fileName);

        const response = await axios.post("http://localhost:3000/images", fileName, {
            headers: { "Content-Type": "application/json" },
        });
        return response.data;
    } catch (error) {
        console.error("Axios download image error:", error);
        throw error;
    }
};
