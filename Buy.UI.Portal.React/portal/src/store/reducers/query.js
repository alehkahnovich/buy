import {
    QUERY_ADD_TERM,
    QUERY_ACCEPTED,
    QUERY_ADD_ATTRIBUTE, 
    QUERY_REMOVE_ATTRIBUTE,
    QUERY_ADD_CAT
} from '../actions/types';

const initial = {
    term: '',
    cat: '',
    facets: [],
    __isDirty: false
};

export default function reducer(state = initial, action) {
    switch(action.type) {
        case QUERY_ACCEPTED:
            return {
                ...state,
                __isDirty: false
            };
        case QUERY_ADD_TERM:
            const isDirty = action.payload !== state.term;
            return {
                ...state,
                term:action.payload,
                __isDirty: isDirty
            };
        case QUERY_ADD_ATTRIBUTE:
            return setAttribute(state, action);
        case QUERY_REMOVE_ATTRIBUTE:
            return removeAttribute(state, action);
        case QUERY_ADD_CAT:
            return {
                ...state,
                cat: action.payload,
                __isDirty: true
            };
        default:
            return state;
    }
}

const setAttribute = (state, action) => {
    switch(action.payload.type) {
        case 'string':
            return setStringFacet(state, action.payload);
        case 'numberrange':
            return setNumberRangeFacet(state, action.payload);
        default: return state;
    }
};

const removeAttribute = (state, action) => {
    switch(action.payload.type) {
        case 'string':
            return removeStringFacet(state, action.payload);
        case 'numberrange':
            return removeNumberRangeFacet(state, action.payload);
        default: return state;
    }
}

const setStringFacet = (state, facet) => {
    const query = {...state};
    const idx = query.facets.findIndex(entry => entry.id === facet.id);
    if (idx === -1) {
        query.facets.push(facet);
        query.__isDirty = true;
        return query;
    }

    const values = query.facets[idx].value;
    for (let index = 0; index < facet.value.length; index++) {
        values.push(facet.value[index]);
    }

    query.__isDirty = true;

    return query;
}

const removeStringFacet = (state, facet) => {
    const query = {...state};
    const idx = query.facets.findIndex(entry => entry.id === facet.id);
    if (idx === -1) return state;

    let values = query.facets[idx].value;
    for (let index = 0; index < facet.value.length; index++) {
        const vindex = values.findIndex(value => value === facet.value[index]);
        if (vindex === -1) continue;
        values.splice(vindex, 1);
    }

    query.__isDirty = true;

    if (values.length === 0) {
        query.facets.splice(idx, 1);
        query.__isDirty = true;
        return query;
    }

    query.facets[idx].value = values;

    return query;
}

const setNumberRangeFacet = (state, facet) => {
    const query = {...state};
    const idx = query.facets.findIndex(entry => entry.id === facet.id);
    if (idx === -1) {
        query.facets.push(facet);
        query.__isDirty = true;
        return query;
    }

    if (!facet.from && !facet.to) {
        query.facets.splice(idx, 1);
        query.__isDirty = true;
        return query;
    }

    query.facets[idx] = facet;
    query.__isDirty = true;
    return query;
}

const removeNumberRangeFacet = (state, facet) => setNumberRangeFacet(state, facet);