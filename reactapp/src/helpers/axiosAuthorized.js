import { variables } from './variables.js';
import { getLocalStorageItem } from './localStorageItems.js';

import axios from 'axios';

export const axiosAuthorized = (method, path, data = null) =>
{
    const token = getLocalStorageItem(variables.ACCESS_TOKEN_NAME);

    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;

    return axios[method](variables.API_URL + path, data);
}