import axios from 'axios';
import {
    CONTENT_REQUESTED,
    CONTENT_RECIEVED,
    CONTENT_FAILED
} from './types';

export function receiveContent(query) {
    return dispatch => {
        dispatch({ type: CONTENT_REQUESTED });
        axios.post('http://localhost:5300/api/search', query)
        .then(response => dispatch({ type: CONTENT_RECIEVED, payload: response.data }))
        .catch(error => dispatch({ type: CONTENT_FAILED, payload: error }))
    }
}