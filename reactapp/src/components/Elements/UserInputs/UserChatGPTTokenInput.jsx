import React from 'react';
import BaseInput from '../BaseInput';

export default function UserChatGPTTokenInput(props){
    return <BaseInput type='text' name="chatGPTToken" required={true}  minLength={6} {...props}/>
}