import { useState, useEffect } from "react";
import { getImage } from "../controllers/imageController";

export const BookImage = ({ id, title }) => {
    const [image, setImage] = useState("");

    useEffect(() => {
        const fetchImage = async () => {
            const imageBlob = await getImage(id);
            if (imageBlob) {

                const url = URL.createObjectURL(imageBlob);
                setImage(url);
            }
        };
        fetchImage();
    }, [id]);
    return (
        < img
            className="book-cover"
            src={image}
            alt={`${title} cover`
            }
        />
    )
}