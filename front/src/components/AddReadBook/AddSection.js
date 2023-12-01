import { MDBCard, MDBCardTitle, MDBInput } from "mdb-react-ui-kit";
import Button from 'react-bootstrap/Button';
import ButtonGroup from 'react-bootstrap/ButtonGroup';
import Dropdown from 'react-bootstrap/Dropdown';
import DropdownItem from "react-bootstrap/esm/DropdownItem";
import DropdownMenu from "react-bootstrap/esm/DropdownMenu";
import Header from "./HeaderAddSection.js";
import React from "react";
import style from './Add.module.css';
import { useState,useEffect,useRef } from "react";
import { Navigate } from "react-router-dom";
 
 
export default function AddSection(props){
    const [path, setPath] = useState('');
    const [callNavigate, setCallNavigate] = useState(false);
 
    const [genre,setGenre] = useState('');
    const [grade,setGrade] = useState('');
    const [name,setName] = useState('');
    const [year,setYear] = useState('');
    const [text,setText] = useState('');
    const [writer,setWriter] = useState();
    const [lastName,setLastName] = useState('');
    const [place,setPlace] = useState('');
    const [birth,setBirth] = useState('');
    const [death,setDeath] = useState('');
    const [bio,setBio] = useState('');
    const [check,setCheck] = useState(false);
 
    const [pisci,setPisci] = useState([]);
    const [genres,setGenres] = useState([]);
 
    const [nameGenre,setNameGenre]=useState([])
    const [IdOfWriter,setIdOfWriter]=useState([])
 
    var userId=localStorage.getItem("userId");
    const BookRef = useRef({grade,genre,text,name,year,writer,lastName,place,birth,death,bio,IdOfWriter,nameGenre})
 
    useEffect(()=>{
        BookRef.current ={
            grade,
            genre,
            text,
            name,
            year,
            writer,
            lastName,
            place,
            birth,
            death,
            bio,
            IdOfWriter,
            nameGenre,
        }
    },[grade,genre,text,name,year,writer,lastName,place,birth,death,bio,IdOfWriter,nameGenre])
 
 
    const changeName = (event) => {
        setName(event.target.value);
      };
 
    const changeYear = (event) => {
        setYear(event.target.value);
      };
 
    const changeText = (event) => {
        setText(event.target.value);
      };
 
      const changeGenre = (event) => {
        setNameGenre(event.target.value);
      };
 
    const changeGenreKey = (eventkey) => {
        var arr = [];             //new storage
        eventkey = eventkey.split(' ');     //split by spaces
        arr.push(eventkey.shift());
        arr.push(eventkey.shift());
        //zeljeno ime parametar za fetch 
        var wantedId = arr[0];
        var wantedName = arr[1];
        setGenre(wantedId);
        setNameGenre(wantedName);
      };
 
      const changeGrade = (eventkey) => {
        setGrade(eventkey);
      };
 
      const changeWriter = (event) => {
        setWriter(event.target.value);
        setCheck(false);
      };
 
 
      const changeWriterKey = (eventkey) => {
        console.log(eventkey);
 
        setCheck(true);
        setLastName('');
        setPlace('');
        setBio('');
        setBirth('');
        setDeath('');
 
        var arr = [];             //new storage
        eventkey = eventkey.split(' ');     //split by spaces
        arr.push(eventkey.shift());
        arr.push(eventkey.shift());
        arr.push(eventkey.shift());
        //zeljeno ime parametar za fetch 
        var wantedId = arr[0];
        var wantedName = arr[1];
        var wantedLastName = arr[2];
 
        setWriter(wantedName+" "+wantedLastName);
        setIdOfWriter(wantedId);
      };
 
      const changeLastName = (event) => {
        setLastName(event.target.value);
      };
 
      const changePlace = (event) => {
        setPlace(event.targeonAddHandlert.value);
      };
 
      const changeBirth = (event) => {
        setBirth(event.target.value);
      };
 
      const changeDeath = (event) => {
        setDeath(event.target.value);
      };
 
      const changeBio = (event) => {
        setBio(event.target.value);
      };
 
      const fetchData = async () =>
      {
          const response = await fetch("https://localhost:44394/api/Writer/GetAllWriters",
          {
              method: 'GET',
              headers: {
                'Content-type': 'application/json;charset=UTF-8'
            }
          });
 
          console.log(response);
          const data = await response.json();
          console.log(data);
          setPisci(data);
 
          const response2 = await fetch("https://localhost:44394/api/Genre/GetAllGenre",
          {
              method: 'GET',
              headers: {
                'Content-type': 'application/json;charset=UTF-8'
            }
          });
          console.log(response2);
          const data2 = await response2.json();
          console.log(data2);
          setGenres(data2);
      };
 
      useEffect(()=>{
        fetchData();
        //copy paste iznad fetch za podatke koji se vracaju i pozvati ovde
    },[]);
 
 
    const onAddHandler = async (event) => {
 
        //BookRef.current.(ovde sta ti treba od ovih iznad)
        //userId ili preko props ili sa storage
        //doda se knjiga pa ocena i pisac da bi se znao id knjige. STA SA GENRE?? KREIRANJE ILI UNAPRED POZNATI IZ BAZE DA VUCE
        //predlog u bookService kada je se dodaje knkiga ispita dal postoji pisac ako ne kreira pa doda knjigu
 
        const response = await fetch("https://localhost:44394/api/Genre/CreateGenre/"+nameGenre,
        {
            method: 'POST',
            headers: {
                'Content-type': 'application/json;charset=UTF-8'
            }
        });
        console.log(response);
        const data = await response.json();
        console.log(data);
        var genreId = data;
        console.log(BookRef.current);

        /* if(check)
        {
 
            //fetch sa poznatim piscem
            var writerId = BookRef.current.IdOfWriter;
            var desc = BookRef.current.text;
            var yearBook = BookRef.current.year;
            var nameBook = BookRef.current.name;
 
            const response = await fetch("https://localhost:44394/api/Book/AddBook/"+nameBook+"/"+yearBook+"/"+desc+"/"+genreId+"/"+writerId,
            {
                method: 'POST',
            });
            console.log(response);
            const data = await response.json();
            console.log(data);
            //HARDCODE USERID = 9
            const response2 = await fetch("https://localhost:44394/api/Mark/AddMark/"+BookRef.current.grade+"/"+userId+"/"+data,
            {
                method: 'POST',
            });
        }
        else */


        //fetch za dodavanje knjige preko izdavacke kuce
        var desc = BookRef.current.text;
        var yearBook = BookRef.current.year;
        var nameBook = BookRef.current.name;

        var nameWriter = BookRef.current.writer;
        var lastnameWriter = BookRef.current.lastName;
        var birthPlace = BookRef.current.place;
        var birthYear = BookRef.current.birth;
        var deathYear = BookRef.current.death;
        var bioWriter = BookRef.current.bio;

        var houseId = localStorage.getItem("houseId");

        const response1 = await fetch("https://localhost:44394/AddBookByPublishingHouse/"+houseId+"/"+nameBook+"/"+yearBook+"/"+desc+"/"+genreId+"/"+nameWriter+"/"+lastnameWriter+"/"+birthPlace+"/"+birthYear+"/"+deathYear+"/"+bioWriter,
        {
            method: 'POST',
            headers: {
                'Content-type': 'application/json;charset=UTF-8'
            }
        });
        console.log(response1);
        const data1 = await response1.json();
        console.log(data1);
 
            
        
        setPath("/Home");
        setCallNavigate(true);
    } 
     
    return(
        <div>
            <Header />
            <MDBCard className={style.AddPostDiv}>
                <div className={style.horizontalLine}>
                    <label>Add new read book</label>
                </div>
                <div>
                    <div className={style.groupDiv}>
                    <span class="input-group-text border-0">Add genre</span>
                    <input value = {nameGenre} onChange={changeGenre} type="text" class="form-control rounded"/>
                    <Dropdown as={ButtonGroup} onSelect={changeGenreKey}>
                        <Button variant="primary">Genres</Button>
                        <Dropdown.Toggle split variant="primary" id="dropdown-split-basic" />
                        <Dropdown.Menu>
                            {genres.map((p)=>(<DropdownItem eventKey={p.id+" "+p.name}>{p.name}</DropdownItem>))} 
                        </Dropdown.Menu>
                    </Dropdown>
                    </div>
                    <div className={style.groupDiv}>
                        <span class="input-group-text border-0">Add title</span>
                        <input type="text" class="form-control rounded" onChange={changeName}/>
                    </div>
                    <div className={style.groupDiv}>
                        <span class="input-group-text border-0">Add year of publication</span>
                        <input type="text" class="form-control rounded" onChange={changeYear}/>
                    </div>
                    <div className={style.groupDiv}>
                        <span class="input-group-text border-0">Add description</span>
                        <textarea class="form-control rounded" aria-label="With textarea" onChange={changeText}></textarea>
                    </div>
                    <div className={style.horizontalLine}>
                    <label>About Writer</label>
                    </div>
                    <div className={style.groupDiv}>
                    <span class="input-group-text border-0">Firstname</span>
                    <input value = {writer} onChange={changeWriter} type="text" class="form-control rounded"/>
                    <Dropdown as={ButtonGroup} onSelect={changeWriterKey}>
                        <Button variant="primary">Writers</Button>
                        <Dropdown.Toggle split variant="primary" id="dropdown-split-basic" />
                        <Dropdown.Menu>
                            {pisci.map((p)=>(<DropdownItem eventKey={p.id+" "+p.firstname+" "+p.lastname}>{p.firstname+" "+p.lastname}</DropdownItem>))} 
                        </Dropdown.Menu>
                    </Dropdown>
                    </div>
                    <div className={style.groupDiv}>
                        <span class="input-group-text border-0">Lastname</span>
                        <input type="text" class="form-control rounded" onChange={changeLastName} disabled={check} value={lastName}/>
                    </div>
                    <div className={style.groupDiv}>
                        <span class="input-group-text border-0">Birth place</span>
                        <input type="text" class="form-control rounded" onChange={changePlace} disabled={check} value={place}/>
                    </div>
                    <div className={style.groupDiv}>
                        <span class="input-group-text border-0">Year of birth</span>
                        <input type="text" class="form-control rounded" onChange={changeBirth} disabled={check} value={birth}/>
                    </div>
                    <div className={style.groupDiv}>
                        <span class="input-group-text border-0">Year of death</span>
                        <input type="text" class="form-control rounded" onChange={changeDeath} disabled={check} value={death}/>
                    </div>
                    <div className={style.groupDiv}>
                        <span class="input-group-text border-0">Biography</span>
                        <input type="text" class="form-control rounded" onChange={changeBio} disabled={check} value={bio}/>
                    </div>
                    <div className={style.horizontalLine}>
                    <label></label>
                    </div>
                    {/* <div className={style.leftDiv}>
                    <span class="input-group-text border-0">Add mark</span>
                    <Dropdown as={ButtonGroup}  onSelect={changeGrade} >
                        <Button variant="primary"></Button>
                        <Dropdown.Toggle split variant="primary" id="dropdown-split-basic" />
                        <Dropdown.Menu>
                            {grades.map((p)=>(<DropdownItem eventKey={p}>{p}</DropdownItem>))} 
                        </Dropdown.Menu>
                    </Dropdown>
                    </div> */}
                    <div className={style.centerBtn}>
                    <Button variant="primary" onClick={onAddHandler}>
                        Add
                    </Button>
                    {callNavigate && <Navigate to={path}/>}
                    </div>
                </div>    
            </MDBCard>
        </div>
    );
}