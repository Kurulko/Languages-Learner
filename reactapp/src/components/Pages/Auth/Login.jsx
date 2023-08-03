import React, { Fragment, useState } from 'react';
import { variables } from '../../../helpers/variables.js';
import { modes } from '../../../helpers/modes';
import { setLocalStorageItemWithExpiration } from '../../../helpers/localStorageItems.js';
import { getErrorsFromResponse } from '../../../helpers/getErrorsFromResponse.js';
import Form from '../../Elements/Form';
import axios from 'axios';
import BaseInput from '../../Elements/BaseInput';
import UserPasswordInput from '../../Elements/UserInputs/UserPasswordInput';

export default function Login() {
  const [user, setUser] = useState({
        name: "",
        password: "", 
        rememberme: false
  });
  const [errors, setErrors] = useState([]);
  
  function handleSubmit(event) {
    event.preventDefault();

    axios[modes.POST](variables.API_URL + 'account/login', user)
    .then(response => {
        const data = response.data;
        setLocalStorageItemWithExpiration(variables.ACCESS_TOKEN_NAME, data.token, data.expirationDays * 24);
        document.location.href = '/';
    })
    .catch(err => setErrors(getErrorsFromResponse(err.response)))
  }

  function ChangeLoginElements() {
    function handleUserChange(event) {
      const {name, value} = event.target;
      setUser({ ...user, [name]: value })    
    }
  
    function handleRememberMeChange(event) {
      setUser({ ...user, "rememberme": event.target.checked })  
    }

    return <Fragment>
        <div>
          <label>Name</label>
          <BaseInput value={user.name} name="name"
              type="text" onChange={handleUserChange} placeholder="Enter name"/>
        </div>
        <br />
        <div>
          <label>Password</label>
          <UserPasswordInput onChange={handleUserChange} value={user.password} placeholder="Enter Password" />
        </div>
        <br />
        <div className="form-check form-switch">
          <label>Remember me?</label>
          <input
              name="rememberme"
              type="checkbox"
              value={user.rememberme}
              onChange={handleRememberMeChange}
              className="form-check-input"
            />  
        </div>
      </Fragment>
  }

  return <Form handleSubmit={handleSubmit} mode='Send' name='Login' errors={errors} Inputs={ChangeLoginElements}/>
}