import React, { Component } from 'react';
import Single from './Behavior/Single';
import Multiple from './Behavior/Multiple';
import './Attribute.scss';

class Attribute extends Component {
    render() {
        let component = null;
        switch(this.props.attribute.behavior) {
            case 'single':
                component = <Single {...this.props} />
                break;
            case 'multiple':
                component = <Multiple {...this.props} />
                break;
            default:
                component = null;
                break;
        }   
        return (
            <div key={this.props.attribute.id} className="col-md-12 mb-3">
                <span className="help-block font-weight-bold text-muted text-uppercase content_control_header">{this.props.attribute.name}</span>
                {component}
            </div>
        );
    }
}

export default Attribute;