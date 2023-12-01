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

export default function App(props) {
    const [path, setPath] = useState('');
    const [callNavigate, setCallNavigate] = useState(false);


    const handleLogOut = (event) => {

        localStorage.removeItem("Username");
        localStorage.removeItem("ID");
        localStorage.removeItem("Role");
        localStorage.removeItem("userId");

        setPath("/login");
        setCallNavigate(true);
    }
    
    return (
        <MDBNavbar light bgColor='light'>
            <MDBContainer fluid>
                <p data-target='#start' className={style.title}>{props.Name}</p> 
                <MDBInputGroup tag="form" className={style.widthText}>
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