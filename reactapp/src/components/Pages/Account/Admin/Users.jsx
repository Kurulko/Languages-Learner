import React, { useState, useEffect, Fragment } from 'react';
import { axiosAuthorized } from '../../../../helpers/axiosAuthorized';
import { modes } from '../../../../helpers/modes';
import { EditModels } from '../../../Elements/EditModels';
import { orderBy } from '../../../../helpers/orderBy';
import { getErrorsFromResponse } from '../../../../helpers/getErrorsFromResponse.js';

export default function Users() {
    const path = 'users';
    const [defaultUser, setDefaultUser] = useState({});
    const [currentUser, setCurrentUser] = useState({});

    const [sort, setSort] = useState({field: null, order: orderBy.ASC});
    const [errors, setErrors] = useState([]);

    useEffect(() => {
        axiosAuthorized(modes.GET, `${path}/current`)
        .then(response => setCurrentUser(response.data))
        .catch(err => setErrors(getErrorsFromResponse(err.response)))
    }, []);

    function getDefaultUser() {
        axiosAuthorized(modes.GET, `${path}/user-by-default`)
        .then(response => setDefaultUser(response.data));

        return defaultUser;
    }

    function UsersHead(){
        function changeSortValue(field) {
            setSort({ field : field, order: sort.order === orderBy.ASC ? orderBy.DESC : orderBy.ASC });
        }

        return <Fragment>
            <th className="cursor-pointer" onClick={() => changeSortValue('name')}>Name</th>
            <th className="cursor-pointer" onClick={() => changeSortValue('email')}>Email</th>
            <th className="cursor-pointer" onClick={() => changeSortValue('chatGPTToken')}>ChatGPT's token</th>
            <th>Roles</th>
            <th>More</th>
        </Fragment>;
    }

    function UsersBody(props){
        const user = props.model;

        const [roles , setRoles] = useState([]);
        
        useEffect(() => {
            axiosAuthorized(modes.GET, 'users/user-roles')
            .then(response => setRoles(response.data))
            .catch(err => setErrors(getErrorsFromResponse(err.response)))
        }, []);

        function impersonate(){
            const formData = new FormData();
            formData.append("usedUserId", user.id);
            axiosAuthorized(modes.PUT, `${path}/change-used-userId`, formData)
            .catch(err => setErrors(getErrorsFromResponse(err.response)))

            document.location.href = '/';
        }
        
        return <Fragment>
            <td>{user.userName}</td>
            <td>{user.email}</td>
            <td className="text-truncate" style={{maxWidth: "150px"}}>{user.chatGPTToken}</td>
            <td className="text-truncate" style={{maxWidth: "150px"}}>{roles.join(',')}</td>
            <td>{currentUser.id !== user.id ?
                <button className='btn btn-outline-warning' onClick={impersonate}>Impersonate</button>
                : null
            }</td>
        </Fragment>;
    }
    function UsersEdit(props){
        const currentUser = props.currentModel, setCurrentUser = props.setCurrentModel;

        function handleCurrentUserChange(event) {
            const {name, value} = event.target;
            setCurrentUser({ ...currentUser, [name]: value })    
        }

        return <div>
            <div className="input-group mb-3">
                <span className="input-group-text">UserName</span>
                <input type="text" className="form-control"
                    value={currentUser.userName} name='userName'
                    onChange={handleCurrentUserChange} 
                    minLength="4" required/>
            </div>
            <div className="input-group mb-3">
                <span className="input-group-text">Email</span>
                <input type="email" className="form-control"
                    value={currentUser.email} name='email'
                    onChange={handleCurrentUserChange} />
            </div>
            <div className="input-group mb-3">
                <span className="input-group-text">ChatGPT's token</span>
                <input type="text" className="form-control"
                    value={currentUser.chatGPTToken} name='chatGPTToken'
                    onChange={handleCurrentUserChange} 
                    minLength="7" required/>
            </div>
        </div>;
    }

    return <EditModels pathToGet={path} pathToEdit={path} name='Users' getDefaultModel={getDefaultUser} Head={UsersHead} 
                Body={UsersBody} Edit={UsersEdit} sort={sort} errors={errors}/>
}

