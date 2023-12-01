import React from 'react';
import style from "./ViewBookCard.module.css";
import { Navigate } from "react-router-dom";
import { useState, useEffect } from 'react';
import {
  MDBCard,
  MDBCardHeader,
  MDBCardBody,
  MDBCardTitle,
  MDBCardText,
  MDBCardFooter,
  MDBBtn
} from 'mdb-react-ui-kit';

export default function App() {

    console.log("USLO U APP");

    const [book, setBook] = useState([]);
    const [writer, setWriter] = useState([]);
    const [genre, setGenre] = useState([]);

    useEffect(() => {
      async function fetchGetBooksByBookName()
        {
            const bookName = localStorage.getItem("bookName");
            const response = await fetch('https://localhost:44394/api/Book/GetBookByName/' + bookName,
                {
                    method: 'GET',
                    headers: {
                        'Content-type': 'application/json;charset=UTF-8'
                    }
                });

                const data = await response.json();

                console.log(data);

                const b = data.map((item) => {
                  return {
                  Title: item.book.title,
                  YearOfPublication: item.book.yearOfPublication,
                  Description: item.book.description
                  };
                });

                setBook(b);

                const w = data.map((item) => {
                  return {
                    Firstname: item.writer.firstname,
                    Lastname: item.writer.lastname,
                    BirthYear: item.writer.birthYear,
                    YearOfDeath: item.writer.yearOfDeath
                  };
                });

                setWriter(w);

                const n = data.map((item) => {
                  return {
                    Name: item.genre.name
                  };
                });

                setGenre(n);
        };
        fetchGetBooksByBookName();
  },[]);

    console.log(book);
    console.log(writer);
    console.log(genre);
  return (
    <div>
         <MDBCard alignment='center'>
      <MDBCardHeader>{writer[0] != null && writer[0].Firstname + " " + writer[0].Lastname + " ( " + writer[0].BirthYear + " - " + writer[0].YearOfDeath + " )"}</MDBCardHeader>
      <MDBCardBody>
        <MDBCardTitle>{book[0] != null && book[0].Title + " ( " + genre[0].Name + " )"}</MDBCardTitle>
        <MDBCardText>{book[0] != null && book[0].Description}</MDBCardText>
      </MDBCardBody>
      <MDBCardFooter>{book[0] != null && "Year of publication: " + book[0].YearOfPublication}</MDBCardFooter>
    </MDBCard>
    </div>
   );
}