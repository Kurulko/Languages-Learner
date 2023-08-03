export default function BaseInput(props){
    const { type, value, name, onChange, disabled, placeholder, ...otherProps } = props;

    return <input type={type} 
        className="form-control"
        value={value} 
        name={name} 
        onChange={onChange}  
        disabled={disabled ?? false}
        placeholder={placeholder}
        {...otherProps}
    />
}