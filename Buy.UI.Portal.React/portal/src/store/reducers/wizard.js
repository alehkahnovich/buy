import {
    WIZARD_SET_CATEGORY,
    WIZARD_PROPERTY_RECEIVED,
    WIZARD_PROPERTY_RESET,
    WIZARD_SET_PROPERTY,
    WIZARD_SET_NAME,
    WIZARD_SET_DESCRIPTION,
    WIZARD_DELETE_PROPERTY,
    WIZARD_CATEGORY_STEP,
    WIZARD_OPTION_STEP,
    WIZARD_PHOTO_STEP,
    WIZARD_PHOTO_UPLOAD_COMPLETED,
    WIZARD_PHOTO_UPLOAD_STARTED,
    WIZARD_ARTIFACT_MODULE_SET,
    WIZARD_ARTIFACT_COMPLETED,
    WIZARD_CREATE_MODULE_STARTED,
    WIZARD_CREATE_MODULE_COMPLETED,
    WIZARD_RESET
} from '../actions/types';

const initial = {
    category: '',
    step: 'category',
    module: {
        name: '',
        cat: '',
        description: '',
        artifacts: [],
        attributes: []
    },
    loading: false,
    module_completed: false,
    artifacts: [],
    attributes: []
};

export default function reducer(state = initial, action) {
    switch(action.type) {
        case WIZARD_RESET:
            return initial;
        case WIZARD_DELETE_PROPERTY:
            return removeProperty(state, action.payload);
        case WIZARD_SET_PROPERTY:
            return setProperty(state, action.payload);
        case WIZARD_SET_NAME:
            const namedcontent = { ...state.module, name: action.payload};
            return {...state, module: namedcontent};
        case WIZARD_SET_DESCRIPTION:
                const descriptioncontent = {...state.module, description: action.payload};
                return {...state,module: descriptioncontent};
        case WIZARD_SET_CATEGORY:
            const categorycontent = {...state.module,cat: action.payload};
            return {...state, module: categorycontent, category: action.payload};
        case WIZARD_PROPERTY_RESET: 
            return {...state, attributes: []};
        case WIZARD_PROPERTY_RECEIVED:
            return {...state, attributes: action.payload};
        case WIZARD_CATEGORY_STEP:
            return {...state, step: 'category'};
        case WIZARD_OPTION_STEP:
            return {...state, step: 'options'};
        case WIZARD_PHOTO_STEP:
            return {...state, step: 'photos'};
        case WIZARD_CREATE_MODULE_COMPLETED:
            return {...state, module_completed: true};
        case WIZARD_CREATE_MODULE_STARTED:
        case WIZARD_PHOTO_UPLOAD_STARTED:
            return {...state, loading: true};
        case WIZARD_ARTIFACT_COMPLETED:
            return updateArtifact(state, action);
        case WIZARD_PHOTO_UPLOAD_COMPLETED: 
            return setArtifacts(state, action);
        case WIZARD_ARTIFACT_MODULE_SET: 
            return setModuleArtifacts(state, action);
        default:
            return state;
    }
}

function setModuleArtifacts(state, action) {
    const entry = {...state.module};
    const artifacts = [...entry.artifacts];
    artifacts.push(action.payload.id);
    entry.artifacts = artifacts;
    return {
        ...state,
        module: entry
    };
}

function updateArtifact(state, action) {
    const artifacts = [...state.artifacts];
    const index = artifacts.findIndex(entry => entry.id === action.payload.id);
    if (index === -1) return state;
    const current = artifacts[index];
    current.url = action.payload.url;
    current.pending = false;
    artifacts[index] = current;
    return {
        ...state,
        artifacts: artifacts
    };
}

function setArtifacts(state, action) {
    const artifacts = [...state.artifacts];
    for (let index = 0; index < action.payload.length; index++) {
        const current = action.payload[index];
        artifacts.push({
            id: current['request_id'],
            pending: true,
            url: null
        });
    }
    return {
        ...state,
        artifacts: artifacts,
        loading: false
    };
}

function setProperty(state, property) {
    switch(property.behavior) {
        case 'single':
            return setSingleProperty(state, property);
        case 'multiple':
            return setMultipleProperty(state, property);
        default:
            return state;
    }
}

function removeProperty(state, property) {
    switch(property.behavior) {
        case 'multiple':
            return removeMultipleProperty(state, property);
        default:
            return state;
    }
}

function removeMultipleProperty(state, property) {
    const options = property.option;
    const content = {...state.module};
    const attributes = [...content.attributes];
    const index = attributes.findIndex(attribute => attribute.id === property.id);
    if (index === -1) 
        return state;
    
    const valueIndex = attributes[index].values.findIndex(entry => entry.id === options.id);

    if (valueIndex === -1)
        return state;

    attributes[index].values.splice(valueIndex, 1);

    content.attributes = attributes;

    return  {
        ...state,
        module: content
    };
}

function setMultipleProperty(state, property) {
    const options = property.option;
    const content = {...state.module};
    const attributes = [...content.attributes];
    const index = attributes.findIndex(attribute => attribute.id === property.id);
    if (index === -1) {
        attributes.push({
            id: property.id,
            values: [options]
        })
        content.attributes = attributes;
        return {...state, module: content};
    }

    attributes[index].values.push(options);
    content.attributes = attributes;

    return  {
        ...state,
        module: content
    };
}

function setSingleProperty(state, property) {
    const options = property.option;
    const content = {...state.module};
    const attributes = [...content.attributes];
    const index = attributes.findIndex(attribute => attribute.id === property.id);
    if (index === -1) {
        attributes.push({
            id: property.id,
            values: [options]
        })
        content.attributes = attributes;
        console.log(content);
        return {...state, module: content};
    }

    attributes[index].values = [options];
    content.attributes = attributes;
    console.log(content);

    return  {
        ...state,
        module: content
    };
}