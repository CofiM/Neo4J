import React from 'react';
import Header from "../Header/Header.js";
import { useState ,useEffect} from "react";
import style from "./PublishingHouse.module.css";
import ViewAllBook from '../Book/ViewAllBook.js';


export default function Community(props){
    const [allPosts, setAllPosts] = useState([]);

    const Id = localStorage.getItem("HouseId");
    const Name = localStorage.getItem("HouseName");
    const place = localStorage.getItem("HousePlace");
    const year = localStorage.getItem("HouseYear");
    const email = localStorage.getItem("HouseEmail");
    const contact = localStorage.getItem("HouseContact")

    // async function fetchBooksHandler()
    // {
    //     const response = await fetch("https://localhost:44394/GetBookByPublishingHouse/" + Id,
    //     {
    //         method: 'GET',
    //         headers: {
    //             'Content-type': 'application/json;charset=UTF-8',
    //         }
    //     });
    //     const data = await response.json();

    //     console.log(data);

    //     const transformedData = data.map((post) => {
    //         return {
    //             ID: book.id,
    //             Title: book.title,
    //             YearOfPublication: book.yearOfPublication,
    //             Description: book.description
    //         };
    //     });
    //     setAllPosts(transformedData);
    // }

    // useEffect(() => {
    //     fetchBooksHandler();
    // },[])

    return (
        <div>
            <Header Name = {Name}  />
            <div  className={style.mainDiv}/* className={style.centralItem} */>
                <div className={style.divLabel}>
                    <label>Published books</label>
                </div>
                <ViewAllBook flag={true}/>
                <div className={style.divInfo}>
                    <label>Contact us: {contact} | {email}</label>
                    <label>Adress: {place}</label>
                    <label>Established in  {year}</label>
                </div>
            </div>
        </div>
        
    );

};