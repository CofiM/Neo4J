import React from 'react';
import style from "./BookCard.module.css";
import { Navigate } from "react-router-dom";
import { useState, useEffect } from 'react';
import {
  MDBCard,
  MDBCardBody,
  MDBCardTitle,
  MDBCardText,
  MDBBtn
} from 'mdb-react-ui-kit';

export default function App() {

    const [book, setBook] = useState(null);

    useEffect(() => {
        async function fetchGetBooksByBookName()
        {
            const bookName = localStorage.getItem("bookName");
            console.log(userId);
            const response = await fetch('https://localhost:44394/api/Book/GetBookByName/' + bookName,
                {
                    method: 'GET',
                    headers: {
                        'Content-type': 'application/json;charset=UTF-8'
                    }
                });
    
            const data = await response.json();
            console.log(data);
        };
        fetchGetBooksByBookName();
    },[]);  
  return (
    <div>
         <MDBCard alignment='center'>
      <MDBCardHeader>Featured</MDBCardHeader>
      <MDBCardBody>
        <MDBCardTitle>Special title treatment</MDBCardTitle>
        <MDBCardText>With supporting text below as a natural lead-in to additional content.</MDBCardText>
        <MDBBtn href='#'>Go somewhere</MDBBtn>
      </MDBCardBody>
      <MDBCardFooter>2 days ago</MDBCardFooter>
    </MDBCard>
    </div>
   );
}