import React from 'react';
import Header from "./HeaderViewBook.js";
import ViewBookCard from "./ViewBookCard.js";


export default function Home(props) {
    
    return(
        <div>
            <Header name="Nesto"/>
            <div>
                <ViewBookCard/>
            </div>
        </div>
    );
}