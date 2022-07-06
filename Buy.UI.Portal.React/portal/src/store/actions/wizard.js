import axios from 'axios';
import {
    WIZARD_SET_CATEGORY,
    WIZARD_PROPERTY_REQUSTED,
    WIZARD_PROPERTY_RECEIVED,
    WIZARD_PROPERTY_FAILED,
    WIZARD_PROPERTY_RESET,
    WIZARD_SET_PROPERTY,
    WIZARD_SET_NAME,
    WIZARD_SET_DESCRIPTION,
    WIZARD_DELETE_PROPERTY,
    WIZARD_CATEGORY_STEP,
    WIZARD_OPTION_STEP,
    WIZARD_PHOTO_STEP,
    WIZARD_PHOTO_UPLOAD_STARTED,
    WIZARD_PHOTO_UPLOAD_COMPLETED,
    WIZARD_PHOTO_UPLOAD_FAILED,
    WIZARD_ARTIFACT_PENDING,
    WIZARD_ARTIFACT_COMPLETED,
    WIZARD_ARTIFACT_ERROR,
    WIZARD_ARTIFACT_MODULE_SET,
    WIZARD_CREATE_MODULE_STARTED,
    WIZARD_CREATE_MODULE_COMPLETED,
    WIZARD_CREATE_MODULE_FAILED,
    WIZARD_RESET
} from './types';

export function reset() {
    return dispatch => dispatch({ type: WIZARD_RESET });
}

export function setCategory(category) {
    return dispatch => {
        if (!category) {
            dispatch({type: WIZARD_PROPERTY_RESET});
            return;
        }
        dispatch({type: WIZARD_SET_CATEGORY, payload: category});
    };
}

export function receiveOptions(category) {
    return dispatch => {
        dispatch({type: WIZARD_PROPERTY_REQUSTED, payload: category});

        axios.get(`http://localhost:5300/api/content/category/${category}/properties`)
        .then(response => dispatch({ type: WIZARD_PROPERTY_RECEIVED, payload: response.data }))
        .catch(error => dispatch({ type: WIZARD_PROPERTY_FAILED, payload: error }))
    };
}

export function createModule(content) {
    return dispatch => {
        dispatch({type: WIZARD_CREATE_MODULE_STARTED, payload: content});
        axios.post(`http://localhost:5300/api/content/module`, content)
        .then(response => dispatch({ type: WIZARD_CREATE_MODULE_COMPLETED, payload: response.data}))
        .catch(error => dispatch({ type: WIZARD_CREATE_MODULE_FAILED, payload: error}));
    }
}

export function setProperty(property) {
    return dispatch => {
        dispatch({type: WIZARD_SET_PROPERTY, payload: property });
    }
}

export function deleteProperty(property) {
    return dispatch => {
        dispatch({type: WIZARD_DELETE_PROPERTY, payload: property });
    }
}

export function setName(name) {
    return dispatch => dispatch({type:WIZARD_SET_NAME, payload: name});
}


export function setDescription(description) {
    return dispatch => dispatch({type:WIZARD_SET_DESCRIPTION, payload: description});
}

export function setCategoryStep() {
    return dispatch => dispatch({ type: WIZARD_CATEGORY_STEP });
}

export function setOptionStep() {
    return dispatch => dispatch({ type: WIZARD_OPTION_STEP });
}

export function setPhotoStep() {
    return dispatch => dispatch({ type: WIZARD_PHOTO_STEP });
}

export function setPhotos(payload) {
    return dispatch => {
        dispatch({type: WIZARD_PHOTO_UPLOAD_STARTED});
        axios.post(`http://localhost:5400/api/content/upload`, payload, {
            'Content-Type': 'multipart/form-data'
        })
        .then(response => {
            dispatch({type: WIZARD_PHOTO_UPLOAD_COMPLETED, payload: response.data});
            for (let index = 0; index < response.data.length; index++) {
                const interval = setInterval(function(accumulator, upld){
                    let current = accumulator;
                    let upldr = upld;
                    return function() {
                        getArtifact(dispatch, upldr, current++, interval);
                    };
                }(0, response.data[index]), 2500);
            }
        })
        .catch(error => dispatch({type:WIZARD_PHOTO_UPLOAD_FAILED, payload:error}));
    }
}

function getArtifact(dispatch, request, accumulator, self) {
    if (accumulator >= 5 || !self) {
        clearInterval(self);
        return;
    }

    dispatch({type: WIZARD_ARTIFACT_PENDING});
    const artifactUri = `http://localhost:5400/api/content/upload/thumbnail/${request['request_id']}`;
    axios
        .get(artifactUri)
        .then(response => {
            if (response.status === 204) 
                return;
            clearInterval(self);
            dispatch({type:WIZARD_ARTIFACT_COMPLETED, payload: { id:request['request_id'], url:artifactUri }});
            dispatch({type:WIZARD_ARTIFACT_MODULE_SET, payload: { id: response.headers['x-content-artifact'] }});
        })
        .catch(error => {
            clearInterval(self);
            dispatch({type:WIZARD_ARTIFACT_ERROR, payload:error});
        });
}