import React from "react";

const MovieItemComponent = props => {

    return(
    <li className="list-group-item">
        <div className="row">
            <label className="form-check mb-2 mr-sm-2">
            {props.name}
            {props.description}
            </label>
        </div>
    </li>
    );
}

export default MovieItemComponent;
