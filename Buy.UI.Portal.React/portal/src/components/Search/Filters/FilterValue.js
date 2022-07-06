import React, { Component } from 'react';
import { Keyword, NumberRange } from './Controls';

class FilterValue extends Component {
    render() {
        switch(this.props.attribute.type) {
            case 'numberrange':
                return (<NumberRange {...this.props}/>);
            default:
                return (<Keyword {...this.props}/>)

        }
    }
}

export default FilterValue;