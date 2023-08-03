import React, { useState, useEffect } from 'react';
import { axiosAuthorized } from '../../../helpers/axiosAuthorized.js';
import { modes } from '../../../helpers/modes';
import { variables } from '../../../helpers/variables.js';
import { getErrorsFromResponse } from '../../../helpers/getErrorsFromResponse.js';
import qs from 'qs';
import BaseInput from '../../Elements/BaseInput';

export function GenerateModelsByLanguage(props) {
    const name = props.name, partPathToGet = props.partPathToGet, pathToEdit = props.pathToEdit, redirectTo = props.redirectTo;
    const modelsOptionsFromChild = props.modelsOptions;

    const [currentLanguageName, setCurrentLanguageName] = useState({});
    const [models, setModels] = useState([]);
    const [modelsOptions, setModelsOptions] = useState({});
    const [errors, setErrors] = useState([]);

    useEffect(() => {
        if(modelsOptionsFromChild !== null)
            setModelsOptions({ ...modelsOptionsFromChild, ["count"]: modelsOptions.count});
    }, [modelsOptionsFromChild]);

    useEffect(() => {
        const _currentLanguageName = JSON.parse(localStorage.getItem(variables.LANGUAGE_NAME))?.name;
        setCurrentLanguageName(_currentLanguageName ?? variables.DEFAULT_LANGUAGE_NAME)
    }, []);

    function handleSubmit(event) {
        event.preventDefault();

        const queryString = qs.stringify(modelsOptions, { encode: false });
        axiosAuthorized(modes.GET, `chatgpt/${partPathToGet}/${currentLanguageName}?${queryString}`)
        .then(response => setModels(response.data))
        .catch(err => setErrors(getErrorsFromResponse(err.response)))
    }

    function handleModelsOptions(event) {
        const {name, value} = event.target;
        setModelsOptions({ ...modelsOptions, [name]: value })    
    }
    
    function handleGeneratedModelInputChange(index, value) {
        setModels(prevModels => 
            prevModels.map((model, i) => 
                 i === index ? model : { ...model, value: value }
          ));
    };
    
    function handleRemoveGeneratedModelField(index) {
        const newModels = models.slice();
        newModels.splice(index, 1);
        setModels(newModels);
    };

    function saveGeneratedModels() {
        models.forEach(model => axiosAuthorized(modes.POST, pathToEdit, model)
            .then(response => setModels(response.data))
            .catch(err => setErrors(getErrorsFromResponse(err.response))));

        document.location.href = `/${redirectTo}`;
    };

    return <div>
        <h1>{name}</h1>
        <br />
        <h5>{
            <ul className='text-danger'>
                {errors.map((error, index) =>
                    <li key={index}>
                        {error}
                    </li>)}
            </ul>
        }</h5>
        <ul>
            {
                models.length > 0 ?
                    <div>
                        {models.map((model, index) => (
                            <div key={index}>
                                <div className="row">
                                    <div className="col-md-6">
                                        <BaseInput value={model.value  ?? ''} 
                                            type="text" onChange={(e) => handleGeneratedModelInputChange(index, e.target.value)} />
                                    </div>
                                    <div className="col-md-3">
                                        <button onClick={() => handleRemoveGeneratedModelField(index)} className="btn btn-warning" type="button">Remove</button>
                                    </div>
                                </div>
                                <br />
                            </div>))}
                        <br />
                        <button onClick={saveGeneratedModels} className="btn btn-warning" type="button">Save</button>
                    </div>
                 : null
            }
        </ul>
        <br />
        <div className="row">
            <div className="col-md-3"></div>
            <div className="col-md-6">
                <form onSubmit={handleSubmit}>
                  <div>
                    <div>
                        <label>Count</label>
                        <BaseInput value={modelsOptions.count} name="count"
                            type="number" onChange={handleModelsOptions} />
                    </div>
                    <br />
                    {props.children}
                    <br />
                    <button className="btn btn-outline-warning">Generate</button> 
                  </div>
                </form>
            </div>
        </div>
    </div>
}