import React, { useState, useEffect, Fragment  } from 'react';
import { Link } from "react-router-dom";
import { axiosAuthorized } from '../../../../helpers/axiosAuthorized';
import { modes } from '../../../../helpers/modes';
import { variables } from '../../../../helpers/variables.js';
import { setLocalStorageItemWithExpiration } from '../../../../helpers/localStorageItems.js';
import { getErrorsFromResponse } from '../../../../helpers/getErrorsFromResponse.js';
import Form from '../../../Elements/Form';
import UserNameInput from '../../../Elements/UserInputs/UserNameInput';
import UserEmailInput from '../../../Elements/UserInputs/UserEmailInput';
import UserChatGPTTokenInput from '../../../Elements/UserInputs/UserChatGPTTokenInput';

export default function UserDetails() {
    const [isEdit, setIsEdit] = useState(false);
    const [user, setUser] = useState({});
    const [errors, setErrors] = useState([]);
    const [isLoading, setIsLoading] = useState(true);

    let isFirst = true;
   
    useEffect(() => {
        if(isFirst)
        {
            isFirst  = false;
            axiosAuthorized(modes.GET, 'users/current')
            .then(response => setUser(response.data))
            .catch(err => setErrors(getErrorsFromResponse(err.response)))
            .finally(() => setIsLoading(false));
        }
      }, []);

    function updateUser() {
        axiosAuthorized(modes.PUT, 'users', user)
        .then(response => { 
            const data = response.data;
            changeMode();
            setLocalStorageItemWithExpiration(variables.ACCESS_TOKEN_NAME, data.token, data.expirationDays * 24);
        })
        .catch(err => setErrors(getErrorsFromResponse(err.response)));
    }

    function changeMode() {
        setIsEdit(!isEdit);
    }

    function handleUserChange(event) {
        const {name, value} = event.target;
        setUser({ ...user, [name]: value })    
    }

    function Links() {
        return <Fragment>
            <br />
            <Link to='/user/edit-password' className='btn btn-outline-warning'>Edit Password</Link>
        </Fragment>
    }

    return <Form handleSubmit={isEdit ? updateUser : changeMode} mode={isEdit ? 'Save' : 'Edit'} name='User' errors={errors} 
        BelowForm={Links} isLoading={isLoading}>
            <div className='row'>
                <h5 className='col'>UserName</h5>
                <div className='col'>
                    <UserNameInput value={user.userName} onChange={handleUserChange} disabled={!isEdit}/>
                </div>
            </div>
            <br />
            <div className='row'>
                <h5 className='col'>Email</h5>
                <div className='col'>
                    <UserEmailInput value={user.email ?? ''} onChange={handleUserChange} disabled={!isEdit}/>
                </div>
            </div>
            <br />
            <div className='row'>
                <h5 className='col'>ChatGPT's token</h5>
                <div className='col'>
                    <UserChatGPTTokenInput value={user.chatGPTToken} onChange={handleUserChange} disabled={!isEdit}/>
                </div>
            </div>
            <br />
     </Form>
}
