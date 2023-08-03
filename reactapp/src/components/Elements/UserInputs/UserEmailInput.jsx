import React from 'react';
import BaseInput from '../BaseInput';

export default function UserPasswordInput(props){
    return <BaseInput type='email' name="email" minLength={8} {...props}/>
}

