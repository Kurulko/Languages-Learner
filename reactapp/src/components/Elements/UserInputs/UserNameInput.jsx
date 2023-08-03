import React from 'react';
import BaseInput from '../BaseInput';

export default function UserNameInput(props){
    return <BaseInput type='text' name="userName" required={true}  minLength={2} placeholder='Username' {...props}/>
}