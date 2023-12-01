import React, { useEffect, useState } from 'react';
import Header from "./Header/Header.js";
import ViewAllBook from './Book/ViewAllBook.js';
import SideBar from './SideBar/SideBar.js';
import style from "./Home.module.css";


export default function Home(props) {
    const [houses,setHouses] = useState([]);

    const fetchData = async () =>
    {
        const response = await fetch("https://localhost:44394/GetAllHouses",
        {
            method: 'GET',
            headers:{'Content-type' : 'application/json; charset=UTF-8'}
        });

        const data = await response.json();
        console.log(data);
        setHouses(data);
    };

    useEffect(() => {
        fetchData();
    },[]);

    return(
        <div>
            <Header/>
            <div className={style.MainPart}>

            <div className={style.sideBar}>
                <SideBar data = {houses}/>
            </div>

            <div className={style.viewBook}>
                <ViewAllBook flag={false}/>
            </div>
            

                
            </div>
        </div>
    );
}