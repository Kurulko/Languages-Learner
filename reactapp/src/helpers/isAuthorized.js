import { getLocalStorageItem } from './localStorageItems.js';
import { variables } from './variables.js';

export const isAuthorized = getLocalStorageItem(variables.ACCESS_TOKEN_NAME) !== null;
