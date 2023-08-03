import React from 'react';
import { Outlet } from "react-router-dom";
import { isAuthorized } from '../../helpers/isAuthorized';
import NavigateToLogin  from '../Elements/NavigateToLogin';

export default function UserWrapper() {
    return isAuthorized ? <Outlet /> : <NavigateToLogin />;
};