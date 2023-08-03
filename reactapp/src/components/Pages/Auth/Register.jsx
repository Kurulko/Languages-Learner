import React, { useState } from 'react';
import { variables } from '../../../helpers/variables.js';
import { modes } from '../../../helpers/modes.js';
import { setLocalStorageItemWithExpiration } from '../../../helpers/localStorageItems.js';
import { getErrorsFromResponse } from '../../../helpers/getErrorsFromResponse.js';
import axios from 'axios';
import BaseInput from '../../Elements/BaseInput';
import UserPasswordInput from '../../Elements/UserInputs/UserPasswordInput';
import UserEmailInput from '../../Elements/UserInputs/UserEmailInput';
import UserChatGPTTokenInput from '../../Elements/UserInputs/UserChatGPTTokenInput';


function Register() {
  const [user, setUser] = useState({
        name: "",
        email:  "",
        password: "", 
        passwordconfirm: "",
        chatgpttoken: "",
        rememberme: false
  });
  const [errors, setErrors] = useState([]);
  const [currentStep, setCurrentStep] = useState(1);
  const countOfSteps = 3, firstStep = 1;

  function handleUserChange(event) {
    const {name, value} = event.target;
    setUser({ ...user, [name]: value })    
  }

  function handleRememberMeChange(event) {
    setUser({ ...user, "rememberme": event.target.checked })  
  }

  function handleSubmit(event) {
    event.preventDefault()
    
    axios[modes.POST](variables.API_URL + 'account/register', user)
    .then(response => {
        const data = response.data;
        setLocalStorageItemWithExpiration(variables.ACCESS_TOKEN_NAME, data.token, data.expirationDays * 24);
        document.location.href = '/';
    })
    .catch(err => setErrors(getErrorsFromResponse(err.response)))
  }
    
  function _next() {
    setCurrentStep(currentStep >= countOfSteps - 1 ? countOfSteps : currentStep + 1);
  }
      
  function _prev() {
    setCurrentStep(currentStep <= firstStep ? firstStep : currentStep - 1);
  }

  function previousButton() {
    if(currentStep !== firstStep){
      return (
        <button type="button" className="btn btn-outline-primary" onClick={_prev}>Back</button>
      )
    }
    return null;
  }

  function nextButton() {
    if(currentStep < countOfSteps){
      return (
        <button type="button" className="btn btn-outline-primary" onClick={_next}>Next</button>        
      )
    }
    return null;
  }
   
  function sendButton(){
    if(currentStep === countOfSteps){
      return (
          <button className="btn btn-outline-warning">Send</button>      
      )
    }
    return null;
  }
 
   
  return (
    <div>
      <h1>Register</h1>
      <hr />
      <h5>Step {currentStep} / {countOfSteps}</h5> 
      <br />
      <h5>{
        <ul className='text-danger'>
            {errors.map((error, index) =>
                <li key={index}>
                    {error}
                </li>)}
        </ul>
      }</h5>
      <div className="row">
        <div className="col-md-3"></div>
        <div className="col-md-6">
          <form onSubmit={handleSubmit}>
            <UserNameAndEmailStep
              currentStep={currentStep} 
              handleChange={handleUserChange}
              name={user.name}
              email={user.email}
            />
            <PasswordStep
              currentStep={currentStep} 
              handleChange={handleUserChange}
              password={user.password}
              passwordconfirm={user.passwordconfirm}
            />
            <ChatGPTTokenStep
              currentStep={currentStep} 
              handleChange={handleUserChange}
              chatgpttoken={user.chatgpttoken}
              rememberme={user.rememberme}
              handleRememberMeChange={handleRememberMeChange}
            />
            <br />
            <p>
              {previousButton()}
              {nextButton()}
              {sendButton()}
            </p>
          </form>
        </div>
      </div>
    </div>
  );
}

function UserNameAndEmailStep(props) {
  if (props.currentStep !== 1) {
    return null
  } 
  return(
    <div>
      <div>
        <label>Name</label>
        <BaseInput value={props.name} name="name"
              type="text" onChange={props.handleChange} placeholder="Enter name"/>
      </div>
      <br />
      <div>
        <label>Email</label>
        <UserEmailInput value={props.email} onChange={props.handleChange} placeholder="Enter email"/>
      </div>
    </div>
  );
}

function PasswordStep(props) {
    if (props.currentStep !== 2) {
      return null
    } 
    return(
      <div>
        <div>
            <label>Password</label>
            <UserPasswordInput onChange={props.handleChange} value={props.password} placeholder="Enter Password" />
        </div>
        <br />
        <div>
            <label>Confirm password</label>
            <UserPasswordInput name="passwordconfirm" onChange={props.handleChange} value={props.passwordconfirm} placeholder="Confirm Password" />
      </div>
    </div>
    );
}

function ChatGPTTokenStep(props) {
  if (props.currentStep !== 3) {
    return null
  } 
  return(
    <div>
      <div>
        <label>ChatGPT token</label>
        <UserChatGPTTokenInput onChange={props.chatgpttoken} value={props.passwordconfirm} placeholder="Enter ChatGPT token" />
      </div>
      <br />
      <div className="form-check form-switch">
        <label>Remember me?</label>
        <input
            name="rememberme"
            type="checkbox"
            value={props.rememberme}
            onChange={props.handleRememberMeChange}
            className="form-check-input"
          />  
      </div>
    </div>
  );
}

export default Register;