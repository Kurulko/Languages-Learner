import React from 'react';
import { ModelsByLanguage } from './ModelsByLanguage';

export default function Rules() {
    return <ModelsByLanguage pathToGet="user-languages-rules" pathToEdit="rules-by-languages" name="Rule"/>
}
