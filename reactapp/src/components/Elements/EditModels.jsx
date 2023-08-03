import React, { useState, useEffect, Fragment, useContext  } from 'react';
import { axiosAuthorized } from '../../helpers/axiosAuthorized';
import { orderBy } from '../../helpers/orderBy';
import { modes } from '../../helpers/modes';
import { Link } from "react-router-dom";
import { useSearchParams } from "react-router-dom";
import { getErrorsFromResponse } from '../../helpers/getErrorsFromResponse.js';
import { LanguageContext } from '../Providers/LanguageProvider'
import Spinner from '../Elements/Spinner'

export function EditModels(props) {
    const { currentLanguage } = useContext(LanguageContext);
    const [searchParams, setSearchParams] = useSearchParams();

    const pathToGet = props.pathToGet;
    const pathToEdit = props.pathToEdit;
    const name = props.name;
    const getDefaultModel = props.getDefaultModel;
    const errorsFromChild = props.errors;

    const [models, setModels] = useState([]);
    const [modalTitle, setModalTitle] = useState('');
    const [errors, setErrors] = useState(errorsFromChild ?? []);
    const [modalErrors, setModalErrors] = useState([]);
    const [currentModel, setCurrentModel] = useState({});
    const [currentPage, setCurrentPage] = useState(getCurrentPageOrDefault());
    const [pageViewModel, setPageViewModel] = useState({});
    const [isFirstRequest, setIsFirstRequest] = useState(true);
    const [isLoading, setIsLoading] = useState(true);
    
    const sortFromChild = props.sort;
    const [sort, setSort] = useState(getCurrentSortOrDefault());

    useEffect(() => {
        const sortFieldFromChild = sortFromChild.field;
        if(sortFieldFromChild !== null)
            refreshCurrentSort(sortFieldFromChild);
    }, [sortFromChild]);

    useEffect(() => {
        setErrors(errors.concat(errorsFromChild));
    }, [errorsFromChild]);

    useEffect(() => {
        refreshList();
    }, [currentLanguage]);

    useEffect(() => {
        refreshList();

        setIsFirstRequest(false);
        setIsLoading(false);
    }, []);

    useEffect(() => {
        refreshList();
        setSearchParams({ page: currentPage, field: sort.field, order: sort.order  });
    }, [currentPage, sort], );

    function getCurrentPageOrDefault() {
        return parseInt(searchParams.get("page") ?? "1");
    }

    function getCurrentSortOrDefault() {
        let sortOrder = orderBy.ASC;

        function setSortOrderIfValid(value)
        {
            if(value !== null && (value === orderBy.DESC || value === orderBy.ASC))
                sortOrder = value;
        }
        setSortOrderIfValid(sortFromChild.order);
        setSortOrderIfValid(searchParams.get("order"));

        let sortField = 'id';

        function setSortFieldIfValid(value)
        {
            if(value !== null)
                sortField = value;
        }
        setSortFieldIfValid(sortFromChild.field);
        setSortFieldIfValid(searchParams.get("field"));

        return {order: sortOrder, field: sortField};
    }

    function dropErrors(){
        setErrors([]);
        setModalErrors([]);
    }

    function refreshList() {
        const isOverOneModel = models.length > 1;

        dropErrors();
        const defaultPageSize = 15;
        const path = pathToGet + ((isFirstRequest || isOverOneModel) ? `?pageNumber=${currentPage}&pageSize=${defaultPageSize}&attribute=${sort.field}&orderBy=${sort.order}` : '');
        axiosAuthorized(modes.GET, path)
        .then(response => { 
            setModels(response.data.models);
            setPageViewModel(response.data.pageViewModel);
        })
        .catch(err => setErrors(getErrorsFromResponse(err.response)))
    }

    function refreshCurrentSort(field) {
        if (sort.field === field) 
            setSort({...sort, order : sort.order === orderBy.ASC ? orderBy.DESC : orderBy.ASC});
        else 
            setSort({field : field, order : orderBy.ASC });
    }

    function refreshCurrentPage(event, page) {
        event.preventDefault();

        setCurrentPage(page);
    }

    function addClick() {
        dropErrors();
        setModalTitle(`Add ${name}`);
        setCurrentModel(getDefaultModel());
    }

    function editClick(model) {
        dropErrors();
        setModalTitle(`Edit ${name}`);
        setCurrentModel(model);
    }

    function methodClick(mode){
        axiosAuthorized(mode, pathToEdit, currentModel)
        .then(refreshList)
        .catch(err => setModalErrors(getErrorsFromResponse(err.response)))
    }

    function createClick() {
        methodClick(modes.POST)
    }

    function updateClick() {
        methodClick(modes.PUT)
    }

    function deleteClick(id) {
        if (window.confirm('R u sure?')) {
            axiosAuthorized(modes.DELETE, `${pathToEdit}/${id}`)
            .then(refreshList)
            .catch(err => setErrors(getErrorsFromResponse(err.response)))
        }
    }

    let isEditCurrentModel = models.find(model => model.id == currentModel.id) !== undefined;
    return (
        <div>
            <h5>{
                <ul className='text-danger'>
                    {errors.map((error, index) =>
                        <li key={index}>
                            {error}
                        </li>)}
                </ul>
            }</h5>
            <button type="button"
                className="btn btn-primary m-2 float-end"
                data-bs-toggle="modal"
                data-bs-target="#exampleModal"
                onClick={addClick}>
                Add {name}
            </button>
            { isLoading ?
                <Spinner />
                : (models.length > 0 ?
                <Fragment>
                    <table className="table table-hover">
                        <thead>
                            <tr>
                                <th className="cursor-pointer" onClick={() => refreshCurrentSort('id')}>Id</th>
                                <props.Head />
                                <th>Options</th>
                            </tr>
                        </thead>
                        <tbody>
                            {models.map(model =>
                                <tr key={model.id}>
                                    <td className="text-truncate" style={{maxWidth: "100px"}}>{model.id}</td>
                                    <props.Body model={model}/>
                                    <td>
                                        <button type="button"
                                            className="btn btn-light mr-1"
                                            data-bs-toggle="modal"
                                            data-bs-target="#exampleModal"
                                            onClick={() => editClick(model)}>
                                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                                                <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                                                <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />
                                            </svg>
                                        </button>

                                        <button type="button"
                                            className="btn btn-light mr-1"
                                            onClick={() => deleteClick(model.id)}>
                                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-trash-fill" viewBox="0 0 16 16">
                                                <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z" />
                                            </svg>
                                        </button>

                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                    <center>
                        {
                            pageViewModel.hasPreviousPage ? 
                            <Link className='btn btn-outline-primary' onClick={(e) => refreshCurrentPage(e, currentPage - 1)} to={`?page=${currentPage - 1}`}>{'< '}Previous</Link>
                            : null
                        }
                        {
                            pageViewModel.hasNextPage ? 
                            <Link className='btn btn-outline-primary' onClick={(e) => refreshCurrentPage(e, currentPage + 1)} to={`?page=${currentPage + 1}`}>Next{' >'}</Link>
                            : null
                        }
                    </center>
                </Fragment>
            : 
                null
            )}
            <div className="modal fade" id="exampleModal" tabIndex="-1" aria-hidden="true">
                <div className="modal-dialog modal-lg modal-dialog-centered">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title">{modalTitle}</h5>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close" />
                        </div>

                        <h5>{
                            <ul className='text-danger'>
                                {modalErrors.map((error, index) =>
                                    <li key={index}>
                                        {error}
                                    </li>)}
                            </ul>
                        }</h5>
                        
                        <div className="modal-body">

                            <div className="input-group mb-3">
                                <span className="input-group-text">Id</span>
                                <input type="text" className="form-control"
                                    value={currentModel.id} name='userName' disabled/>
                            </div>
                            
                            <props.Edit currentModel={currentModel} setCurrentModel={setCurrentModel} />
                            
                            {
                                <button type="button"
                                        className="btn btn-primary float-start" 
                                        onClick={isEditCurrentModel ? updateClick : createClick}>
                                    {isEditCurrentModel ? 'Update' : 'Create'}
                                </button>
                            }
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}