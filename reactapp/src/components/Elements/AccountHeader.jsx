import React, { useState, useEffect, Fragment } from 'react';
import ButtonLink from './ButtonLink';
import Logout from "../Pages/Auth/Logout";
import { isAuthorized } from '../../helpers/isAuthorized';
import { axiosAuthorized } from '../../helpers/axiosAuthorized';
import { modes } from '../../helpers/modes';

export default function AccountHeader() {
    const [isImpersonating , setIsImpersonating] = useState(false);

    useEffect(() => {
        axiosAuthorized(modes.GET, `users/is-impersonating`)
        .then(response => setIsImpersonating(response.data));
    }, []);

    const authLinks = [
        ["Register", "/register"],
        ["Login", "/login"],
    ];
    
    function finishImpersonating(){
        axiosAuthorized(modes.DELETE, `users/drop-used-userId`)
        .then(() => setIsImpersonating(false), () => {
            alert('Failed');
        })
        
        document.location.href = '/';
    }

    return <Fragment>{isAuthorized ? 
        <Fragment>
            <Logout/>
            {isImpersonating ? 
                <button class="btn btn-warning" onClick={finishImpersonating}>Finish</button>
                : null
            }
        </Fragment>
        :
        <ul className="navbar-nav">
            {authLinks.map(userLink => {
                const [name, path] = userLink;
                return <li className="nav-item m-1" key={name}>
                    <ButtonLink path={path} name={name} />
                </li>
            })}
        </ul>
    }</Fragment>;
}
