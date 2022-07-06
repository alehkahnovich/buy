import React, { useEffect } from 'react';
import Typography from '@material-ui/core/Typography';
import { connect } from 'react-redux';
import { receive, create, update, remove } from '../store/actions/properties';
import PropertiesGrid from './Grid/Properties/PropertiesGrid';

const Properties = function(props) {
    const receive = props.receive;
    useEffect(() => {
        receive();
    }, [receive]);

    return (
        <div>
            <Typography variant="h3" gutterBottom>
                Properties
            </Typography>
            <PropertiesGrid 
                properties={props.properties}
                onCreate={props.create}
                onUpdate={props.update}
                onDelete={props.remove}
                isLoading={props.loading} />
        </div>
    );
}


const mapping = (state) => {
    return {
        properties: state.property.properties,
        loading: state.property.loading
    }
}

const dispatchers = dispatch => {
    return {
        receive: () => dispatch(receive()),
        create: (payload) => dispatch(create(payload)),
        update: (payload) => dispatch(update(payload)),
        remove: (key) => dispatch(remove(key))
    }
}

export default connect(mapping, dispatchers)(Properties);