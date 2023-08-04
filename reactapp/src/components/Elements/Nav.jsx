import React, { useState, useEffect, Fragment  } from 'react';
import ButtonLink from './ButtonLink';
import { axiosAuthorized } from '../../helpers/axiosAuthorized';
import { modes } from '../../helpers/modes';
import { isAuthorized } from '../../helpers/isAuthorized';

export default function Nav() {
    const [roles, setRoles] = useState([]);
    const [isAdmin, setIsAdmin] = useState(false);

    useEffect(() => {
        if(isAuthorized)
        {
            axiosAuthorized(modes.GET, 'users/user-roles')
            .then(response => setRoles(response.data));
        }
    }, []);

    useEffect(() => {
        setIsAdmin(roles.find(r => r.toLowerCase() === 'admin') !== undefined)
    }, [roles]);

    const userLinks = [
        ["Home", "/"],
        ["Sentences", "/sentences"],
        ["Words", "/words"],
        ["Idioms", "/idioms"],
        ["Rules", "/rules"],
    ];
    const adminLinks = [
        ["Users", "/users"],
        ["Roles", "/roles"],
        ["Languages", "/languages"],
    ];


    return <Fragment>
        {!isAuthorized ? null : 
        <nav className="navbar navbar-expand-sm bg-light navbar-dark">
            <ul className="navbar-nav">
                {(isAdmin ? userLinks.concat(adminLinks) : userLinks).map(userLink => {
                    const [name, path] = userLink;
                    return <li className="nav-item m-1" key={name}>
                        <ButtonLink path={path} name={name} />
                    </li>
                })}
            </ul>
        </nav>}</Fragment>;
}