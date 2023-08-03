import React from 'react';
import { Navigate } from "react-router-dom";

export default function NavigateToLogin() {
    return <Navigate to="/login" />;
};