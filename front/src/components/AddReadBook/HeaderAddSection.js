import React from 'react';
import {useState} from 'react';
import { Navigate } from "react-router-dom";
import style from "./HeaderAddSection.module.css";
import {
  MDBContainer,
  MDBNavbar,
  MDBBtn,
  MDBInputGroup  
} from 'mdb-react-ui-kit';

export default function App() {
    const [path, setPath] = useState('');
    const [callNavigate, setCallNavigate] = useState(false);
    const [genre, setGenre] = useState('');
    const [writer, setWriter] = useState('');


    const onChangeGenreHandler = (event) => {
        setGenre(event.target.value);
        console.log(genre);
    }

    const onChangeWriterHandler = (event) => {
        setWriter(event.target.value);
        console.log(writer);
    }

    const onSearchWriterHandler = (event) => {
        setPath("/Topic");
        setCallNavigate(true);
    }


    const onSearchGenreHandler = (event) => {
        setPath("/Topic");
        setCallNavigate(true);
    }

    const handleLogOut = (event) => {

        localStorage.removeItem("Token");
        localStorage.removeItem("Username");
        localStorage.removeItem("ID");
        localStorage.removeItem("Role");
        localStorage.removeItem("userId");

        setPath("/login");
        setCallNavigate(true);
    }

    const handleGetToHomePage = (event) =>
    {
        setPath("/Home");
        setCallNavigate(true);
    }
    
    return (
        <MDBNavbar light bgColor='light'>
            <MDBContainer fluid>
                <p data-target='#start' className={style.title}>Books</p> 
                <MDBInputGroup tag="form" className={style.widthText}>
                </MDBInputGroup>
                <MDBBtn
                    color='secondary'
                    data-target='#navbarToggleExternalContent'
                    aria-controls='navbarToggleExternalContent'
                    aria-expanded='false'
                    aria-label='Toggle navigation'
                    onClick={handleGetToHomePage}
                >
                    Home
                    {callNavigate && <Navigate to={path} variant="body2"/>}
                </MDBBtn>

                <MDBBtn
                    color='secondary'
                    data-target='#navbarToggleExternalContent'
                    aria-controls='navbarToggleExternalContent'
                    aria-expanded='false'
                    aria-label='Toggle navigation'
                    onClick={handleLogOut}
                >
                    Log out
                    {callNavigate && <Navigate to={path} variant="body2"/>}
                </MDBBtn>
            </MDBContainer>
        </MDBNavbar>
    );
}