import React, { useState } from 'react';
import style from "./SideBar.module.css"
import { Navigate, NavLink } from "react-router-dom";

export default function SideBarItem(name) {
        const [callNavigate, setCallNavigate] = useState(false);
        const [path, setPath] = useState('');


        const handleOnClickTopic = (event) => {
                setCallNavigate(true);
                console.log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                console.log(name);
                setPath("/PublishingHouse");
                localStorage.setItem("HouseId", name.ID);
                localStorage.setItem("HouseName", name.name);
                localStorage.setItem("HouseContact", name.contact);
                localStorage.setItem("HouseEmail",name.email);
                localStorage.setItem("HousePlace",name.place)
                localStorage.setItem("HouseYear",name.year)
        }

        console.log(name);
        return (
                <a 
                        onClick={handleOnClickTopic}
                > 
                {name.name}
                {callNavigate && <Navigate to={path}/> }
                </a>
        );
  }