import React, { useState, useEffect, Fragment, useContext  } from 'react';
import { axiosAuthorized } from '../../../helpers/axiosAuthorized';
import { modes } from '../../../helpers/modes';
import { EditModels } from '../../Elements/EditModels';
import { orderBy } from '../../../helpers/orderBy';
import { variables } from '../../../helpers/variables';
import { getErrorsFromResponse } from '../../../helpers/getErrorsFromResponse.js';
import BaseInput from '../../Elements/BaseInput';
import { LanguageContext } from '../../Providers/LanguageProvider'

export function ModelsByLanguage(props) {
    const { currentLanguage } = useContext(LanguageContext);
    const isThereCurrentLanguage = currentLanguage !== null;

    const pathToUser = 'users';
    const pathToGet = `${pathToUser}/${props.pathToGet}/${isThereCurrentLanguage ? 'by-name/' + currentLanguage.name : null}`;
    const pathToEdit = props.pathToEdit;
    const name = props.name;

    const [languages, setLanguages] = useState([]);
    const [sort, setSort] = useState({field: null, order: orderBy.ASC});
    const [errors, setErrors] = useState([]);

    useEffect(() => {
        if(!isThereCurrentLanguage)
            intializeLanguages();
    }, []);

    function intializeLanguages() {
        const pathToLanguages = 'languages';
        axiosAuthorized(modes.GET, pathToLanguages)
        .then(response => setLanguages(response.data.models))
        .catch(err => setErrors(getErrorsFromResponse(err.response)))
    }

    function getDefaultModel() {
        return {id : 0, value : '', languageId: isThereCurrentLanguage ? currentLanguage.id : 0 , language : isThereCurrentLanguage ? currentLanguage : null};
    }

    function ModelsHead(){
        function changeSortValue(field) {
            setSort({ field : field, order: sort.order === orderBy.ASC ? orderBy.DESC : orderBy.ASC });
        }

        return <Fragment>
            <th className="cursor-pointer" onClick={() => changeSortValue('value')}>{name}</th>
            {
                isThereCurrentLanguage ? null : <th className="cursor-pointer" onClick={() => changeSortValue('language.Name')}>Language</th>
            }
        </Fragment>;
    }

    function ModelsBody(props){
        const model = props.model;
        return <Fragment>
            <td>{model.value}</td>
            {
                isThereCurrentLanguage ? null : <td>{model.language.name}</td>
            }
        </Fragment>;
    }

    function ModelsEdit(props){
        const currentModel = props.currentModel, setCurrentModel = props.setCurrentModel;

        function changeCurrentValue(e) {
            setCurrentModel({ ...currentModel, value: e.target.value });
        }
    
        function changeCurrentLanguageId(e) {
            setCurrentModel({ ...currentModel, languageId: e.target.value });
        }

        return <div>
            <div className="input-group mb-3">
                <span className="input-group-text">Sentence</span>
                <BaseInput value={currentModel.value} 
                    type="text" onChange={changeCurrentValue} />
            </div>
            {
                isThereCurrentLanguage ? null :
                <div className="input-group mb-3">
                    <span className="input-group-text">Language</span>
                    <select value={currentModel.languageId} onChange={changeCurrentLanguageId} className="form-control">
                        <option value="">Choose language</option>
                        {languages.map((language) => (
                            <option key={language.id} value={language.id}>
                                {language.name}
                            </option>
                        ))}
                    </select>
                </div>
            }
        </div>;
    }

    return <EditModels pathToGet={pathToGet} pathToEdit={pathToEdit} name={name} getDefaultModel={getDefaultModel} Head={ModelsHead} 
                Body={ModelsBody} Edit={ModelsEdit} sort={sort} errors={errors}/>
}