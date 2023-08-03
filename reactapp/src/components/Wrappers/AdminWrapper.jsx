import React, { useState, useEffect, Suspense   } from 'react';
import { Outlet } from "react-router-dom";
import { isAuthorized } from '../../helpers/isAuthorized';
import { axiosAuthorized } from '../../helpers/axiosAuthorized';
import { modes } from '../../helpers/modes';
import NavigateToLogin  from '../Elements/NavigateToLogin';
import Spinner  from '../Elements/Spinner';

export default function AdminWrapper() {

    const [isAdmin, setIsAdmin] = useState(false);
    const [isLoading, setIsLoading] = useState(true);

    useEffect(() => {
        if(isAuthorized)
        {
            axiosAuthorized(modes.GET, 'users/user-roles')
            .then(response => {
                const roles = response.data;
                setIsAdmin(roles.includes('Admin'))
                setIsLoading(false);
            });
        }
    }, []);

    if (isLoading) 
        return <Spinner />;

    if(!isAuthorized || !isAdmin)
        return <NavigateToLogin/>;

    return <Outlet />;
};

