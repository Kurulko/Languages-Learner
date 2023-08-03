import { variables } from '../../../helpers/variables.js';
import { axiosAuthorized } from '../../../helpers/axiosAuthorized.js';
import { modes } from '../../../helpers/modes';
import UserName from '../Account/User/UserName';

export default function Logout(){

    function logout(){
        axiosAuthorized(modes.POST, 'account/logout')
        localStorage.removeItem(variables.ACCESS_TOKEN_NAME);
        document.location.href = '/login';
    }

    return <span>
        {<UserName />}
        <span className="nav-item m-1" key='logout'>
            <button className="btn btn-light btn-outline-primary" onClick={logout}>
                Logout
            </button>
        </span>
    </span>
}