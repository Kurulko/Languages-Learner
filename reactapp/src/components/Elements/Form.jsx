import React from 'react';
import Spinner from './Spinner'

export default function Form(props){
    const errorsFromChild = props.errors;
    const BelowForm = props.BelowForm;
    const isLoading = props.isLoading;
    
    function handleSubmit(event) {
        event.preventDefault()
        props.handleSubmit(event);
    }

    return <div>
        <h3>{props.name}</h3>
        <hr /><br />
        <div className="row">
            <div className="col-md-3">
                <h5>{
                    <ul className='text-danger'>
                        {errorsFromChild.map((error, index) =>
                            <li key={index}>
                                {error}
                            </li>)}
                    </ul>
                }</h5>
            </div>
            { (isLoading ?? false) ?
                <Spinner /> :
                <div className="col-md-6">
                    <form onSubmit={handleSubmit}>
                        {props.children}
                        <br />
                        <center>
                            <button className="btn btn-outline-warning">{props.mode}</button>     
                        </center>
                    </form>
                    <br />
                    {BelowForm === undefined ? null : <BelowForm />}
                </div>
            }
        </div>
    </div>
}