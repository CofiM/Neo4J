import React  from 'react';
import { Navigate } from 'react-router-dom';
import { Route, Routes, BrowserRouter as Router } from "react-router-dom";
import Login from './components/Login.js';
import Home from "./components/Home.js";
import ViewBook from "./components/Book/ViewBook/ViewBook.js";
import AddReadBook from "./components/AddReadBook/AddSection.js";
import ViewBookByWriter from "./components/Book/ViewBook/ViewBookByWriter.js";
import ViewBookByGenre from "./components/Book/ViewBook/ViewBookByGenre.js";
import HomePublishingHouse  from "./components/HomePublishingHouse.js";
import PublishingHouse from "./components/PublishingHouse/PublishingHouse.js";

function App() {

  return (
        <Router>
          <Routes>
            <Route exact path="/" element={<Login/>} /> 
            <Route path="/login" element={<Login/>} />
            <Route path="/Home" element={<Home/>} />
            <Route path="/ViewBook" element={<ViewBook/>} />
            <Route path="/Add" element={<AddReadBook/>} />
            <Route path='/BookByWriter' element = {<ViewBookByWriter/>} />
            <Route path='/BookByGenre' element = { <ViewBookByGenre/> } />
            <Route path='/HomePublishingHouse' element={<HomePublishingHouse/>} />
            <Route path='/PublishingHouse' element={<PublishingHouse/>} />
          </Routes>
        </Router> 
  );
}

export default App;

