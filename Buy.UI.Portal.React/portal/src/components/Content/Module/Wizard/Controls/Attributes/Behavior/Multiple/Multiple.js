import React from 'react';
import { Number, Text } from './Control';
class Multiple extends React.Component {
    render() {
        switch(this.props.attribute.control) {
            case 'number':
                return <Number {...this.props}/>
            case 'text':
                return <Text {...this.props} />
            default:
                return null;
        }
    }
}

export default Multiple;