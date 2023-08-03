import React from 'react';
import BaseInput from '../BaseInput';

export default function UserPasswordInput(props){
    return <BaseInput type='password' name={props.name ?? 'password'} required={true} minLength={8} {...props} />
}

