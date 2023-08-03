import React from 'react';
import { ModelsByLanguage } from './ModelsByLanguage';

export default function Words() {
    return <ModelsByLanguage pathToGet="user-languages-words" pathToEdit="words-by-languages" name="Word"/>
}