import React, { useState } from 'react';
import { axiosAuthorized } from '../../../../helpers/axiosAuthorized';
import { modes } from '../../../../helpers/modes';
import { getErrorsFromResponse } from '../../../../helpers/getErrorsFromResponse.js';
import Form from '../../../Elements/Form';
import UserPasswordInput from '../../../Elements/UserInputs/UserPasswordInput';


export default function EditUserPassword() {
    const [password, setPassword] = useState({oldPassword: '', newPassword:'', confirmNewPassword:''});
    const [errors, setErrors] = useState([]);

    function handlePasswordChange(event) {
        const {name, value} = event.target;
        setPassword({ ...password, [name]: value }) 
    }

    function updateUser() {
        axiosAuthorized(modes.PUT, 'users/password', password)
        .then(() => document.location.href = '/user-details')
        .catch(err => setErrors(getErrorsFromResponse(err.response)));
    }

    function ChangePasswordElements() {
        return <div>
            <div>
                <label>Old Password</label>
                <UserPasswordInput name="oldPassword" onChange={handlePasswordChange} value={password.oldPassword} placeholder="Enter Old Password" />
                    {/* autoComplete="old-password" */}
            </div>
            <br />
            <div>
                <label>New Password</label>
                <UserPasswordInput name="newPassword" onChange={handlePasswordChange} value={password.newPassword} placeholder="Enter New Password" />
            </div>
            <br />
            <div>
                <label>Confirm New Password</label>
                <UserPasswordInput name="confirmNewPassword" onChange={handlePasswordChange} value={password.confirmNewPassword} placeholder="Enter Confirm New Password" />
            </div>
            <br />
        </div>
    }

    return <Form handleSubmit={updateUser} mode='Edit' name='Edit Password' errors={errors} Inputs={ChangePasswordElements}/>
}