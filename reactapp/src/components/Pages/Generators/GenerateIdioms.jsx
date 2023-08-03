import React, { useState, Fragment } from 'react';
import { GenerateModelsByLanguage } from './GenerateModelsByLanguage';
import BaseInput from '../../Elements/BaseInput';

export default function GenerateIdioms() {

    const [idiomsOptions, setIdiomsOptions] = useState({
        word: null,
    });

    function EditIdiom(){
        function handleIdiomsOptions(event) {
            const {name, value} = event.target;
            setIdiomsOptions({ ...idiomsOptions, [name]: value })    
        }
    
        return <Fragment>
            <div>
                <label>Word</label>
                <BaseInput value={idiomsOptions.word} name="word"
                    type="text" onChange={handleIdiomsOptions} placeholder="Enter word"/>
            </div> 
            <br />
        </Fragment>;
    }

    return <GenerateModelsByLanguage name='Idioms' partPathToGet='idioms' pathToEdit='idioms-by-languages' redirectTo='idioms' 
                EditModel={EditIdiom} modelsOptions={idiomsOptions}/>
}