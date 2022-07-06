import React from 'react';
import { Number, Year, Text } from './Control';

class Single extends React.Component {
    render() {
        switch(this.props.attribute.control) {
            case 'number':
                return <Number {...this.props}/>
            case 'year':
                return <Year {...this.props} />
            case 'text':
                return <Text {...this.props} />
            default:
                return null;
        }
    }
}

export default Single;