import React from 'react';
import {useState} from 'react';
import { Navigate } from "react-router-dom";
import style from "./Header.module.css";
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
        localStorage.setItem("writer", writer);
        setPath("/BookByWriter");
        setCallNavigate(true);
    }


    const onSearchGenreHandler = (event) => {
        localStorage.setItem("genre", genre);
        setPath("/BookByGenre");
        setCallNavigate(true);
    }

    const handleLogOut = (event) => {

        localStorage.removeItem("Username");
        localStorage.removeItem("ID");
        localStorage.removeItem("Role");
        localStorage.removeItem("userId");

        setPath("/login");
        setCallNavigate(true);
    }

    const handleAddReadBook = (event) =>
    {
        setPath("/Home");
        setCallNavigate(true);
    }
    
    return (
        <MDBNavbar light bgColor='light'>
            <MDBContainer fluid>
                <p data-target='#start' className={style.title}>FindBooks</p> 
                <MDBInputGroup tag="form" className={style.widthText}>
                    <input 
                        className='form-control' 
                        placeholder="Type writer" 
                        aria-label="Search" 
                        type='Search' 
                        onChange={onChangeWriterHandler}
                    />
                    <MDBBtn 
                        color='secondary' 
                        className={style.buttonClass}
                        onClick={onSearchWriterHandler}
                    >
                        Search
                        {callNavigate && <Navigate to={path} variant="body2"/>}
                    </MDBBtn>

                    <input 
                        className='form-control' 
                        placeholder="Type genre" 
                        aria-label="Search" 
                        type='Search' 
                        onChange={onChangeGenreHandler}
                    />
                    <MDBBtn 
                        color='secondary' 
                        className={style.buttonClass}
                        onClick={onSearchGenreHandler}
                    >
                        Search
                        {callNavigate && <Navigate to={path} variant="body2"/>}
                    </MDBBtn>
                </MDBInputGroup>

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