import "../styles/bookCard.css"

export const Genre = ({ name, description, onClick }) => {
    return (
        <div className="book-card" onClick={onClick}>
            <h3>{name}</h3>
            <p>{description}</p>
        </div>
    );
};