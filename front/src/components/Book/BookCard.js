import React from 'react';
import style from "./BookCard.module.css";
import { Navigate } from "react-router-dom";
import Button from 'react-bootstrap/Button';
import ButtonGroup from 'react-bootstrap/ButtonGroup';
import { useState, useEffect } from 'react';
import Dropdown from 'react-bootstrap/Dropdown';
import DropdownItem from "react-bootstrap/esm/DropdownItem";
import DropdownMenu from "react-bootstrap/esm/DropdownMenu";
import {
  MDBCard,
  MDBCardBody,
  MDBCardTitle,
  MDBCardText,
  MDBBtn
} from 'mdb-react-ui-kit';

export default function App(props) {

    const [path, setPath] = useState("");
    const [callNavigate, setCallNavigate] = useState(false);
    const grades = [1, 2, 3, 4, 5];
    const [grade, setGrade] = useState(1);

    const [allBooks, setAllBooks] = useState([]);
    useEffect(() => {
      async function fetchGetBooksByUser()
      {
          const userId = localStorage.getItem("userId");
          console.log(userId);
          const response = await fetch('https://localhost:44394/GetBooksByUser/' + userId,
              {
                  method: 'GET',
                  headers: {
                      'Content-type': 'application/json;charset=UTF-8'
                  }
              });
  
          const data = await response.json();
          console.log(data);
          const books = data.map((book)=>{
              return{
                  ID: book.bookId,
                  Title: book.title,
                  YearOfPublication: book.yearOfPublication,
                  Description: book.description,
                  WriterName: book.firstName,
                  WriterLastname: book.lastName,
                  Genre: book.genreName,
                  Mark: book.mark

              };
          });
          setAllBooks(books);
          console.log(books);
      };

      async function fetchGetBooksByPublishingHouse()
      {
          const id = localStorage.getItem("HouseId");
          const response = await fetch('https://localhost:44394/GetBookByPublishingHouse/' + id,
              {
                  method: 'GET',
                  headers: {
                      'Content-type': 'application/json;charset=UTF-8'
                  }
              });
  
          const data = await response.json();
          console.log(data);
          const books = data.map((book)=>{
              return{
                ID: book.id,
                Title: book.title,
                YearOfPublication: book.yearOfPublication,
                Description: book.description,
              };
          });
          setAllBooks(books);
          console.log(books);
        };
        console.log("FLAAG" +  props.flags);
        const role = localStorage.getItem("Role");
        if(role == 'U' && props.flags == false)
            fetchGetBooksByUser();
        if(role == 'H' && props.flags == false)
            fetchGetBooksByPublishingHouse();
        if(role == "U" && props.flags == true)
            fetchGetBooksByPublishingHouse();
            
    },[]);  

  const changeGrade = (eventkey) => {
    setGrade(eventkey);
  };

  async function onAddHandler(bookTitle, grade) {
    const userId = localStorage.getItem("userId");
    const response = await fetch("https://localhost:44394/api/Mark/AddMark/" + grade + "/" + userId + "/" + bookTitle,
            {
                method: 'POST',
            });
  };

  async function onAddReadHandler(bookTitle) {
    const userId = localStorage.getItem("userId");
    const response = await fetch("https://localhost:44394/ReadBook/" + bookTitle + "/" + userId,
            {
                method: 'POST',
            });
  };

  return (
    <div className={style.divView}>
        { allBooks.map((book) => (
        <div className={style.divMain}>
            { (localStorage.getItem("Role") == 'U' && props.flags == false) &&  <MDBCard alignment='center'>
                <MDBCardBody>
                    <MDBCardTitle>{book.Title + " - " + book.WriterName + " " + book.WriterLastname + " (" + book.Genre + ")"}</MDBCardTitle>
                    <MDBCardText>
                        {book.Description}
                    </MDBCardText>
                    <MDBCardText>
                        {"Ocena: " + book.Mark}
                    </MDBCardText>
                    <div className={style.leftDiv}>
                    <span class="input-group-text border-0">Add mark</span>
                    <Dropdown as={ButtonGroup}  onSelect={changeGrade} >
                        <Button variant="primary"></Button>
                        <Dropdown.Toggle split variant="primary" id="dropdown-split-basic" />
                        <Dropdown.Menu>
                            {grades.map((p)=>(<DropdownItem eventKey={p}>{p}</DropdownItem>))} 
                        </Dropdown.Menu>
                    </Dropdown>
                    </div>
                    <div className={style.centerBtn}>
                    <Button variant="primary" onClick={() => onAddHandler(book.Title, grade)}>
                        Add
                    </Button>
                    </div>
                </MDBCardBody>
            </MDBCard>}
            {((localStorage.getItem("Role") == 'U' && props.flags == true)) && <MDBCard alignment='center'>
                <MDBCardBody>
                    <MDBCardTitle>{book.Title}</MDBCardTitle>
                    <MDBCardText>
                        {book.Description}
                    </MDBCardText>
                    <div className={style.leftDiv}>
                    </div>
                    <div className={style.centerBtn}>
                    <Button variant="primary" onClick={() => onAddReadHandler(book.Title, grade)}>
                        Add
                    </Button>
                    </div>
                </MDBCardBody>
            </MDBCard>}
            {((localStorage.getItem("Role") == 'H' && props.flags == false)) && <MDBCard alignment='center'>
                <MDBCardBody>
                    <MDBCardTitle>{book.Title}</MDBCardTitle>
                    <MDBCardText>
                        {book.Description}
                    </MDBCardText>
                    <div className={style.leftDiv}>
                    </div>
                </MDBCardBody>
            </MDBCard>}
        </div>
        ))}
    </div>
   );
}