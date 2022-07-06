import {
    ROOT_BUCKETS_REQUESTED,
    ROOT_BUCKETS_RECIEVED,
    ROOT_BUCKETS_TOOGLE_MENU
} from '../actions/types';

const initial = {
    roots: [],
    open: false
};

export default function reducer(state = initial, action) {
    switch(action.type) {
        case ROOT_BUCKETS_TOOGLE_MENU:
            return {...state, open: !state.open};
        case ROOT_BUCKETS_REQUESTED:
            return state;
        case ROOT_BUCKETS_RECIEVED: 
            return {...state, roots:action.payload};
        default: 
            return state;
    }
}