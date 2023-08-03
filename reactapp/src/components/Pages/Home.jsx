import React, { useEffect, useState, Fragment } from 'react';
import { axiosAuthorized } from '../../helpers/axiosAuthorized.js';
import { modes } from '../../helpers/modes';
import { getErrorsFromResponse } from '../../helpers/getErrorsFromResponse.js';

export default function Home(){
    const [userName, setUserName] = useState();
    const [errors, setErrors] = useState([]);
    
    useEffect(() => {
        axiosAuthorized(modes.GET, 'users/current-username')
        .then(response => setUserName(response.data))
        .catch(err => setErrors(getErrorsFromResponse(err.response)))
    }, []);

    return <Fragment>
        <h5>{
            <ul className='text-danger'>
                {errors.map((error, index) =>
                    <li key={index}>
                        {error}
                    </li>)}
            </ul>
        }</h5>
        <br />
        <h1>Hello {userName}!</h1>
    </Fragment>;
}