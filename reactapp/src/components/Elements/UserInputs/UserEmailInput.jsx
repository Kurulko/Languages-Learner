import React from 'react';
import BaseInput from '../BaseInput';

export default function UserPasswordInput(props){
    return <BaseInput type='email' name="email" required={true} minLength={8} {...props}/>
}

