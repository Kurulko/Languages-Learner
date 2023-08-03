import React from 'react';
import { Link } from 'react-router-dom';
import { ModelsByLanguage } from './ModelsByLanguage';

export default function Sentences() {
    return <div>
        <Link to='generate' className="btn btn-light btn-outline-primary">Generate Sentences</Link>
        <br />
        <ModelsByLanguage pathToGet="user-languages-sentences" pathToEdit="sentences-by-languages" name="Sentence"/>
    </div>
}
