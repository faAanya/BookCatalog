import React, { useState, useEffect } from "react";
import { registerUser, loginUser } from "../../controllers/userController";
import { useNavigate } from "react-router-dom";

export function AuthPage() {
    const [mode, setMode] = useState("login"); // "login" или "register"
    const [message, setMessage] = useState("");
    const navigate = useNavigate();

    // Проверяем токен при монтировании компонента
    useEffect(() => {
        const token = localStorage.getItem("token");
        if (token) {
            navigate("/books");
        }
    }, [navigate]);

    const handleLogin = (token) => {
        localStorage.setItem("token", token);
        setMessage("");
        navigate("/books"); // редирект на /books
    };

    return (
        <div>
            {mode === "login" ? (
                <>
                    <LoginForm onLogin={handleLogin} />
                    <p>
                        Нет аккаунта?{" "}
                        <button onClick={() => setMode("register")}>Зарегистрироваться</button>
                    </p>
                </>
            ) : (
                <>
                    <RegisterForm
                        onRegistered={() => {
                            setMessage("Регистрация прошла успешно! Войдите, пожалуйста.");
                            setMode("login");
                        }}
                    />
                    <p>
                        Уже есть аккаунт?{" "}
                        <button onClick={() => setMode("login")}>Войти</button>
                    </p>
                </>
            )}
            {message && <p style={{ color: "green" }}>{message}</p>}
        </div>
    );
}

function RegisterForm({ onRegistered }) {
    const [username, setUsername] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError(
            typeof err.response?.data === "string"
                ? err.response.data
                : JSON.stringify(err.response?.data) || "Ошибка входа"
        );
        try {
            await registerUser({ username, email, password });
            onRegistered();
            setUsername("");
            setEmail("");
            setPassword("");
        } catch (err) {
            setError(err.response?.data || "Ошибка регистрации");
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Регистрация</h2>
            <input
                type="text"
                placeholder="Имя пользователя"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                required
            /><br />
            <input
                type="email"
                placeholder="Email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
            /><br />
            <input
                type="password"
                placeholder="Пароль"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
            /><br />
            <button type="submit">Зарегистрироваться</button>
            {error && <p style={{ color: "red" }}>{error}</p>}
        </form>
    );
}

function LoginForm({ onLogin }) {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError("");
        try {
            const data = await loginUser({ username, password });
            onLogin(data.token);
            setUsername("");
            setPassword("");
        } catch (err) {
            setError(err.response?.data || "Ошибка входа");
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <h2>Вход</h2>
            <input
                type="text"
                placeholder="Имя пользователя"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                required
            /><br />
            <input
                type="password"
                placeholder="Пароль"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
            /><br />
            <button type="submit">Войти</button>
            {error && <p style={{ color: "red" }}>{error}</p>}
        </form>
    );
}
