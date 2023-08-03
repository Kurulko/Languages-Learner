import React, { useState, useEffect, useContext   } from 'react';
import AccountHeader from './AccountHeader';
import { variables } from '../../helpers/variables';
import { axiosAuthorized } from '../../helpers/axiosAuthorized';
import { modes } from '../../helpers/modes';
import { LanguageContext } from '../Providers/LanguageProvider';
import { isAuthorized } from '../../helpers/isAuthorized';

export default function Header() {
    const path = 'users/user-current-language';
    const [languages, setLanguages] = useState([]);
    const { currentLanguage, setCurrentLanguage } = useContext(LanguageContext);

    useEffect(() => {
        if(isAuthorized)
            initializeCurrentLanguage();
        initializeLanguages();
    }, []);

    useEffect(() => {
        if(currentLanguage?.name != undefined);
            localStorage.setItem(variables.LANGUAGE_NAME, JSON.stringify(currentLanguage));
    }, [currentLanguage]);

    function initializeCurrentLanguage(){
        const _currentLanguage = JSON.parse(localStorage.getItem(variables.LANGUAGE_NAME));
        if(_currentLanguage?.name === undefined)
            axiosAuthorized(modes.GET, path)
            .then(response => setCurrentLanguage(response.data));
        else
            setCurrentLanguage(_currentLanguage);
    }

    function initializeLanguages(){
        axiosAuthorized(modes.GET, 'languages')
        .then(response => setLanguages(response.data.models));
    }

    function changeCurrentLanguage(e){
        const chosenLanguage = languages.find(l => l.id == e.target.value);

        if(chosenLanguage != null) {
            setCurrentLanguage(chosenLanguage);

            axiosAuthorized(modes.PUT, path, chosenLanguage)
            .then(() => null, () => {
                alert('Failed');
            })
        }
        else{
            setCurrentLanguage(null);
        }
    }
    
    return <div className="row">
        <div className='col'>
            <h3 className="d-flex justify-content-center m-3">
                {variables.APPLICATION_NAME}
            </h3>
        </div>
        <div className='col'>
            <span className="d-flex justify-content-center m-3">
                <nav className="navbar navbar-expand-sm bg-light navbar-dark">
                    <AccountHeader />
                    <select value={currentLanguage.id} onChange={changeCurrentLanguage} className="form-control" style={{width:'30%'}}>
                        <option disabled>Choose language</option>
                        <option value={null}>All</option>
                        {languages.map((language) => (
                            <option key={language.id} value={language.id}>
                                {language.name}
                            </option>
                        ))}
                    </select>
                </nav>
            </span>
        </div>
    </div>;
}