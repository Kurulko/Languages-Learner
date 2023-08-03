import React, { useState, Fragment    } from 'react';
import { EditModels } from '../../../Elements/EditModels';
import { orderBy } from '../../../../helpers/orderBy';
import BaseInput from '../../../Elements/BaseInput';

export default function Languages() {
    const path = 'languages';
    const [sort, setSort] = useState({field: null, order: orderBy.ASC});

    function getDefaultLanguage() {
        return {id: 0, name: ''};
    }

    function LanguagesHead(){
        function changeSortValue(field) {
            setSort({ field : field, order: sort.order === orderBy.ASC ? orderBy.DESC : orderBy.ASC });
        }

        return <Fragment>
            <th className="cursor-pointer" onClick={() => changeSortValue('name')}>Name</th>
        </Fragment>;
    }

    function LanguagesBody(props){
        return <Fragment>
            <td>{props.model.name}</td>
        </Fragment>;
    }

    function LanguagesEdit(props){
        const currentLanguage = props.currentModel, setCurrentLanguage = props.setCurrentModel;

        function changeCurrentValue(e) {
            setCurrentLanguage({ ...currentLanguage, name: e.target.value });
        }

        return <div className="input-group mb-3">
            <span className="input-group-text">Name</span>
            <BaseInput type="text" value={currentLanguage.name} onChange={changeCurrentValue} minLength="3" required />
        </div>;
    }

    return <EditModels pathToGet={path} pathToEdit={path} name='Languages' getDefaultModel={getDefaultLanguage} Head={LanguagesHead} 
                Body={LanguagesBody} Edit={LanguagesEdit} sort={sort}/>
}

