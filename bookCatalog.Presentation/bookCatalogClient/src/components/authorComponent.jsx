import "../styles/bookCard.css"
export const Author = ({
    id,
    firstName,
    lastName,
    biography,
    onClick
}) => {
    return (
        <div className="book-details" onClick={() => onClick(
            firstName,
            lastName,
            biography)}>
            <p><strong>First Name:</strong> {firstName ?? "—"}</p>
            <p><strong>Last Name:</strong> {lastName ?? "—"}</p>
            <p><strong>Biography</strong> {biography ?? "—"}</p>
        </div >
    );
};