import React from 'react';
import style from "../BookCard.module.css";
import { Navigate } from "react-router-dom";
import { useState, useEffect } from 'react';
import Header from "../../Header/Header.js";
import {
  MDBCard,
  MDBCardBody,
  MDBCardTitle,
  MDBCardText,
  MDBBtn
} from 'mdb-react-ui-kit';

export default function App() {

    const[shown, setShown] = useState(false);
    const [path, setPath] = useState("");
    const [callNavigate, setCallNavigate] = useState(false);
    const [isNotFound, setIsNotFound] = useState(false);

    const [allBooks, setAllBooks] = useState([]);
    useEffect(() => {
      async function fetchGetBooksByGenre()
      {
          const name = localStorage.getItem("genre");
          console.log("--------------");
          console.log(name);
          const ID = localStorage.getItem("userId");
          
          const response = await fetch('https://localhost:44394/GetBooksForGenre/' + name + '/' + ID,
              {
                  method: 'GET',
                  headers: {
                      'Content-type': 'application/json;charset=UTF-8'
                  }
               });
               //.then(x => {
            //     if (!x.ok)
            //     {
            //         setIsNotFound(true);
            //         return;
            //     }
            //   })
        console.log(response);
          const data = await response.json();
         // console.log(data);
          const books = data.map((book)=>{
              return{
                  ID: book.id,
                  Title: book.title,
                  YearOfPublication: book.yearOfPublication,
                  Description: book.description
              };
          });
          setAllBooks(books);
          if(books.length == 0)
          {
            setShown(true);
          }
          console.log(books);
      };
      fetchGetBooksByGenre();
  },[]);  

  const onViewBook = (event) => {
    const name = event.currentTarget.id;
    localStorage.setItem("bookName", name);

    setPath("/ViewBook");
    setCallNavigate(true);

    //console.log("USLOOOOO");
}

  return (
    <div>
        <Header />
        <div className={style.divView}>
        { allBooks.map((book) => (
        <div className={style.divMain}>
            <MDBCard alignment='center'>
                <MDBCardBody>
                    <MDBCardTitle>{book.Title}</MDBCardTitle>
                    <MDBCardText>
                        {book.Description}
                    </MDBCardText>
                </MDBCardBody>
            </MDBCard>
        </div>
        ))}
        {shown && <p> No read book for this writer </p>}
        {isNotFound && <p> Not found </p>}
    </div>
    </div>
   );
}