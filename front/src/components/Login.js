import React, { useState } from 'react';
import { Navigate, NavLink } from "react-router-dom";
import { useNavigate } from "react-router-dom";

import {
  MDBContainer,
  MDBTabs,
  MDBTabsItem,
  MDBTabsLink,
  MDBTabsContent,
  MDBTabsPane,
  MDBBtn,
  MDBIcon,
  MDBInput,
  MDBCheckbox
}
from 'mdb-react-ui-kit';

function App() {
  const [callNavigate, setCallNavigate] = useState(false);
  const [textUsername, setTextUsername] = useState('');
  const [path, setPath] = useState('');
  const [textPassword, setTextPassword] = useState('');
  const [textRepeatPassword, setTextRepeatPassword] = useState('');
  const [textEmail, setTextEmail] = useState('');
  const [paragraphIsShown, setParagraphIsShown] = useState(false)
  const [justifyActive, setJustifyActive] = useState('tab1');
  const [role, setRole] = useState('');


  const onChangeUsernameHandler = (event) => {
    setTextUsername(event.target.value);
  }

  const onChangePasswordHandler = (event) => {
    setTextPassword(event.target.value);
  }

  const onChangeRepeatPasswordHandler = (event) => {
    setTextRepeatPassword(event.target.value);
  }

  const onChangeEmailHandler = (event) => {
    setTextEmail(event.target.value);
  }

  const handleSubmitSignIn = (event) =>{
    console.log("ULAZIM!");
    event.preventDefault();

    if(textUsername === null || textPassword === null)
    {
      setParagraphIsShown(true);
    }

    console.log(textUsername, textPassword);
    fetchLoginClient();

    // if (role == "U")
    // {
    //   setPath("/Home");
    //   setCallNavigate(true);
    // }

    // if(role == "H")
    // {
    //   setPath("/HomePublishingHouse");
    //   setCallNavigate(true);
    // }
  }

  async function fetchLoginClient(){
    console.log("Ulazim u  fetch");
    const response = await fetch("https://localhost:44394/api/UserContoller/GetUserAccount/" + textUsername + "/" + textPassword,
    {
      method: 'GET',
      headers: {
        'Content-type': 'application/json;charset=UTF-8'
      }
    });
    let data = await response.json();

    console.log(data);

    if (data.role == 'U')
    {
      setRole(data.role);
      const username = data.username;
      const id = data.id;
      localStorage.setItem("Username", username);
      localStorage.setItem("userId", id);
      localStorage.setItem("Role", 'U');

      setPath("/Home");
      setCallNavigate(true);
    }
    if (data.role == 'H')
    {
      setRole(data.role);
      const houseName = data.name;
      const id = data.id;
      localStorage.setItem("HouseName", houseName);
      localStorage.setItem("houseId", id);
      localStorage.setItem("Role", 'H');

      setPath("/HomePublishingHouse");
      setCallNavigate(true);
    }
  }

  const handleSubmitSignUp = (event) => {
    console.log("ULAZIM!");
    event.preventDefault();

    if(textUsername === null || textPassword === null || 
      textRepeatPassword === null || !/^[a-zA-Z0-9+_.-]+@[a-z]+[.]+[c]+[o]+[m]$/.test(textEmail))
    {
      setParagraphIsShown(true);
    }

    console.log(textUsername, textPassword, textRepeatPassword, textEmail);
    handleJustifyClick('tab1')

    fetchSignUpClient();
  }

  async function fetchSignUpClient(){
    const response = await fetch("https://localhost:44394/api/UserContoller/CreateUserAccount/" + 
                  textUsername + "/" + textEmail + "/" + textPassword + "/" + textRepeatPassword,
    {
      method: 'POST',
      body: JSON.stringify({title: 'Successful add'}),
      headers: {
        'Content-Type': 'application/json'
      }
    });

    const data = await response.json();
    console.log(data);
  }

  const handleJustifyClick = (value) => {
    if (value === justifyActive) {
      return;
    }

    setJustifyActive(value);
  };

  return (
    <MDBContainer className="p-3 my-5 d-flex flex-column w-50">

      <MDBTabs pills justify className='mb-3 d-flex flex-row justify-content-between'>
        <MDBTabsItem>
          <MDBTabsLink 
            onClick={() => handleJustifyClick('tab1')} active={justifyActive === 'tab1'}>
            Login
          </MDBTabsLink>
        </MDBTabsItem>
        <MDBTabsItem>
          <MDBTabsLink onClick={() => handleJustifyClick('tab2')} active={justifyActive === 'tab2'}>
            Register
          </MDBTabsLink>
        </MDBTabsItem>
      </MDBTabs>

      <MDBTabsContent>

        <MDBTabsPane show={justifyActive === 'tab1'}>

          <div className="text-center mb-3">
            <p>Sign in</p>
          </div>

          <MDBInput 
            wrapperClass='mb-4' 
            label='Username' 
            id='form1' 
            type='email' 
            onChange={onChangeUsernameHandler}
          />
          <MDBInput 
            wrapperClass='mb-4' 
            label='Password' 
            id='form2' 
            type='password' 
            onChange={onChangePasswordHandler}
          />

          <MDBBtn 
            className="mb-4 w-100"
            onClick={handleSubmitSignIn}
          >  
            Sign in
            {callNavigate && <Navigate to={path} variant="body2"></Navigate>} 
          </MDBBtn>
          {paragraphIsShown && <p style={{color:"red"}}> Invalid username or password</p>}
          <p 
            className="text-center" 
            onClick={() => handleJustifyClick('tab2')} 
            active={justifyActive === 'tab2'}
          >
            Not a member? 
          <a href="#!"> Register</a></p>

        </MDBTabsPane>

        <MDBTabsPane show={justifyActive === 'tab2'}>

          <div className="text-center mb-3">
            <p>Sign up:</p>
          </div>


          <MDBInput 
            wrapperClass='mb-4' 
            label='Username' 
            id='form1' 
            type='text'
            onChange={onChangeUsernameHandler}
          />
          <MDBInput 
            wrapperClass='mb-4' 
            label='Email' 
            id='form1' 
            type='email'
            onChange={onChangeEmailHandler}
          />
          <MDBInput 
            wrapperClass='mb-4' 
            label='Password' 
            id='form1' 
            type='password'
            onChange={onChangePasswordHandler}
          />
          <MDBInput 
            wrapperClass='mb-4' 
            label='Repeat password' 
            id='form1' 
            type='password' 
            onChange={onChangeRepeatPasswordHandler}
          />

          {paragraphIsShown && <p style={{color:"red"}}> Invalid input! Try again </p>}
          <MDBBtn 
            className="mb-4 w-100"
            onClick={handleSubmitSignUp}
            active={justifyActive === 'tab1'}
          >
            Sign up
          </MDBBtn>
          
        </MDBTabsPane>

      </MDBTabsContent>

    </MDBContainer>
  );
}

export default App;