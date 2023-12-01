import React from 'react';
import style from "./SideBar.module.css";
import SideBarItem from "./SideBarItem.js";
import { useEffect,useState } from "react";

export default function SideBar(props) {
    
    console.log("DATA " + props.data);
    console.log("SIDE BARRRRRRRRRRRRRRRRR");
    console.log(props);
    return (
        <div className={style.sidebar}>            
            <label className={style.LeftMargin}>Publishing House</label>
            { props.data.map((p)=>(
                <div>
                    <SideBarItem style = "marginRight:50px" 
                        name={p.name}
                        ID = {p.id}
                        year = {p.yearOfEstablishment}
                        email = {p.email}
                        place = {p.place}
                        contact = {p.contact}
                    />
                </div>
            ))}
        </div>
    );
}