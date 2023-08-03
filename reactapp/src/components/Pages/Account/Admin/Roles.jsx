import React, { useState, useEffect, Fragment  } from 'react';
import { EditModels } from '../../../Elements/EditModels';
import { axiosAuthorized } from '../../../../helpers/axiosAuthorized';
import { modes } from '../../../../helpers/modes';
import { orderBy } from '../../../../helpers/orderBy';
import { getErrorsFromResponse } from '../../../../helpers/getErrorsFromResponse.js';
import BaseInput from '../../../Elements/BaseInput';

export default function Roles() {
    const path = 'roles';
    const [defaultRole, setDefaultRole] = useState({});
    
    const [sort, setSort] = useState({field: null, order: orderBy.ASC});
    const [errors, setErrors] = useState([]);

    function getDefaultRole() {
        axiosAuthorized(modes.GET, `${path}/role-by-default`)
        .then(response => setDefaultRole(response.data))
        .catch(err => setErrors(getErrorsFromResponse(err.response)))
        return defaultRole;
    }

    function RolesHead(){
        function changeSortValue(field) {
            setSort({ field : field, order: sort.order === orderBy.ASC ? orderBy.DESC : orderBy.ASC });
        }

        return <Fragment>
            <th className="cursor-pointer" onClick={() => changeSortValue('name')}>Name</th>
            <th>Users</th>
        </Fragment>;
    }

    function RolesBody(props){
        const roleName = props.model.name;

        const [users , setUsers] = useState([]);
        
        useEffect(() => {
            axiosAuthorized(modes.GET, `users/users-by-role/${roleName}`)
            .then(response => setUsers(response.data))
            .catch(err => setErrors(getErrorsFromResponse(err.response)))
        }, []);
    
        return <Fragment>
            <td>{roleName}</td>
            <td className="text-truncate" style={{maxWidth: "150px"}}>{users.map(u => u.userName).join(',')}</td>
        </Fragment>;
    }

    function RolesEdit(props){
        const currentRole = props.currentModel, setCurrentRole = props.setCurrentModel;

        function changeCurrentValue(e) {
            setCurrentRole({ ...currentRole, name: e.target.value });
        }

        return <div className="input-group mb-3">
            <span className="input-group-text">Name</span>
            <BaseInput type="text" value={currentRole.name} onChange={changeCurrentValue} minLength="3" required />
        </div>;
    }

    return <EditModels pathToGet={path} pathToEdit={path} name='Roles' getDefaultModel={getDefaultRole} Head={RolesHead} 
                Body={RolesBody} Edit={RolesEdit} sort={sort} errors={errors}/>
}
