import axios from "axios";

export const registerUser = async (user) => {
    try {
        const response = await axios.post(`http://localhost:3000/auth/register`, user, {
            headers: { "Content-Type": "application/json" },
        });
        return response.data; // например, "User registered"
    } catch (error) {
        console.error("Axios registerUser error:", error);
        throw error;
    }
};

export const loginUser = async (credentials) => {
    try {
        const response = await axios.post(`http://localhost:3000/auth/login`, credentials, {
            headers: { "Content-Type": "application/json" },
        });
        return response.data; // { token: "JWT_TOKEN_HERE" }
    } catch (error) {
        console.error("Axios loginUser error:", error);
        throw error;
    }
};
