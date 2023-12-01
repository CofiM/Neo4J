import React from 'react';
import Header from "./Header/HeaderPublishingHouse.js";
import ViewAllBook from './Book/ViewAllBook.js';


export default function Home(props) {
    
    return(
        <div>
            <Header/>
            <div>
                <ViewAllBook flag = {false}/>
            </div>
        </div>
    );
}