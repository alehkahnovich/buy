import React, { Component } from 'react';
import Attribute from './Attributes';
import { Collapse, Button } from 'reactstrap';
import './Controls.scss'

class Controls extends Component {
    constructor(props) {
        super(props);
        this.state = {
            open: false
        };
    }
    onRemove = (property) => {
        this.props.removeProperty(property);
    }
    onChange = (property) => {
        this.props.setProperty(property);
    }
    toggle = (event) => {
        event.preventDefault();
        this.setState({ open: !this.state.open });
    }
    render() {
        if (this.props.attributes.length === 0)
            return null;

        const attributes = this.props.attributes.map((attribute, index) => {
            const separator = this.props.attributes.length - 1 !== index
            ? (<hr className="my-4"/>)
            : null
            return (
                <div key={attribute.id}>
                    <Attribute attribute={attribute} onChange={this.onChange} onRemove={this.onRemove}/>
                    {separator}
                </div>
            );
        });

        return (
            <div>
                <div className="mb-3">
                    <Button onClick={this.toggle} color="link font-weight-bold small text-uppercase">Дополнительные опции</Button>
                </div>
                <Collapse isOpen={this.state.open} >
                    {attributes}
                </Collapse>
            </div>
        );
    }
}

export default Controls;