import React, { useEffect, useState } from 'react';
import { Link } from "react-router-dom";
import { axiosAuthorized } from '../../../../helpers/axiosAuthorized';
import { modes } from '../../../../helpers/modes';
import { getErrorsFromResponse } from '../../../../helpers/getErrorsFromResponse.js';

export default function UserName(){
    const [userName, setUserName] = useState();
    
    useEffect(() => {
        axiosAuthorized(modes.GET, 'users/current-username')
        .then(response => setUserName(response.data))
        .catch(err => getErrorsFromResponse(err.response))
    }, []);

    return <Link to="user-details" className='btn btn-primary'>Hello, {userName}</Link>;
}

