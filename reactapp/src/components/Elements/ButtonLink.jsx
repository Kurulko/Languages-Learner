import React from 'react';
import { NavLink } from "react-router-dom";

export default function ButtonLink(props){
    return <NavLink className="btn btn-light btn-outline-primary" to={props.path}>
            {props.name}
    </NavLink>;
}
