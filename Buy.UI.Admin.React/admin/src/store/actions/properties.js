import axios from 'axios';
import config from '../../config';
import {
    PROPERTIES_REQUESTED, 
    PROPERTIES_RECEIVED, 
    PROPERTIES_FAILED,
    PROPERTIES_CREATED,
    PROPERTIES_CREATED_REQUESTED,
    PROPERTIES_CREATED_FAILED,
    PROPERTIES_PATCH_REQUESTED,
    PROPERTIES_PATCH,
    PROPERTIES_PATCH_FAILED,
    PROPERTIES_DELETE_REQUESTED,
    PROPERTIES_DELETE,
    PROPERTIES_DELETE_FAILED
} from './types';

const configuration = config();

export function receive() {
    return (dispatch) => {
        dispatch({ type:PROPERTIES_REQUESTED });
        axios
        .get(`${configuration.content.uri}/api/content/property`)
        .then(response => dispatch({
            type:PROPERTIES_RECEIVED,
            payload:response.data
        }))
        .catch(error => dispatch({
            type:PROPERTIES_FAILED,
            payload:error
        }))
    }
}

export function create(payload) {
    return (dispatch) => {
        dispatch({ type:PROPERTIES_CREATED_REQUESTED });
        axios
        .post(`${configuration.content.uri}/api/content/property`, payload)
        .then(response => dispatch({
            type:PROPERTIES_CREATED,
            payload:response.data
        }))
        .catch(error => dispatch({
            type:PROPERTIES_CREATED_FAILED,
            payload:error
        }))
    }
}

export function update(payload) {
    return (dispatch) => {
        dispatch({ type:PROPERTIES_PATCH_REQUESTED });
        axios
        .put(`${configuration.content.uri}/api/content/property`, payload)
        .then(response => dispatch({
            type:PROPERTIES_PATCH,
            payload:payload
        }))
        .catch(error => dispatch({
            type:PROPERTIES_PATCH_FAILED,
            payload:error
        }))
    }
}

export function remove(key) {
    return dispatch => {
        dispatch({ type: PROPERTIES_DELETE_REQUESTED });
        axios
        .delete(`${configuration.content.uri}/api/content/property/${key}`)
        .then(_ => dispatch({ type: PROPERTIES_DELETE, payload:key }))
        .catch(error => dispatch({ type: PROPERTIES_DELETE_FAILED, payload: error }));
    }
}