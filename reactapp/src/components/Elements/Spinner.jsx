export default function Spinner(){
    return <div className="text-center">
        <div className="spinner-border spinner-circle text-primary" role="status">
        <span className="visually-hidden">Loading...</span>
        </div>
        <p>Loading...</p>
    </div>;
}