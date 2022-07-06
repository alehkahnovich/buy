import React, { Component } from 'react';
import { Container } from 'reactstrap';
import FormControl from './Wizard/Form';
import './Module.scss'

class Module extends Component {
    render() {
        return (
            <Container fluid className="content_main">
                <FormControl {...this.props}/>
            </Container>
        );
    }
}

export default Module;