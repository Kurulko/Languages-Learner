export function getErrorsFromResponse(response) {
    const errors = [];

    const data = response.data.errors;
    for(let error in data)
        data[error].forEach(e => errors.push(e));
    console.error("ERROR: " + errors);
    
    return errors;
}
