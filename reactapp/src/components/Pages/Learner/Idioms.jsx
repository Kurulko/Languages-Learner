import React from 'react';
import { Link } from 'react-router-dom';
import { ModelsByLanguage } from './ModelsByLanguage';

export default function Idioms() {
    return <div>
        <Link to='generate' className="btn btn-light btn-outline-primary">Generate Idioms</Link>
        <br />
        <ModelsByLanguage pathToGet="user-languages-idioms" pathToEdit="idioms-by-languages" name="Idiom"/>
    </div>
}
