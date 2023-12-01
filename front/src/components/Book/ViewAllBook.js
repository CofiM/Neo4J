import React from "react";
import BookCard from "./BookCard.js"


const ViewAllBook = (props) =>
{
    return(
        <div>
            <BookCard flags={props.flag}/>
        </div>
    );
}

export default ViewAllBook;