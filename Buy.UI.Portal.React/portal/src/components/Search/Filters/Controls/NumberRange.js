import React, { Component } from 'react';
import { Form, FormGroup, Col, Container } from 'reactstrap';
import { QUERY_ADD_ATTRIBUTE } from '../../../../store/actions/types';
import classNames from 'classnames';
import './NumberRange.scss';

const interval = 1500;
const regexp = /^\d+$/;
const initial = { from: '', to: '' };
class NumberRange extends Component {
    constructor(props) {
        super(props);
        this.state = initial;
        this.timer = null;
    }
    componentDidUpdate(prevProps) {
        if (this.props.isReset !== prevProps.isReset) {
            this.props.setDirty(false);
            this.setState(initial, () => {
                this.onFacetSet();
            });
        }
    }
    setTimer = () => {
        if (this.timer !== null)
            clearTimeout(this.timer);
        this.timer = setTimeout(this.onFacetSet, interval);

    }
    onFacetSet = () => {
        const action = { type: QUERY_ADD_ATTRIBUTE };
        this.props.onFacetSet({
            id: this.props.facetKey,
            type: this.props.attribute.type,
            from: this.state.from || this.props.attribute.from,
            to: this.state.to || this.props.attribute.to
        }, action);
    }
    setDirty = () => {
        const from = this.state.from || this.props.attribute.from;
        const to = this.state.to || this.props.attribute.to;
        if (from === this.props.attribute.from && to === this.props.attribute.to) {
            this.props.setDirty(false);
            return;
        }
        this.props.setDirty(true);
    }
    setFrom = (e) => {
        const value = e.currentTarget.value;
        if (!this.isValid(value))
            return;

        this.setState({from: !value ? value : parseInt(value)});
        this.setUpdate();
    }
    setTo = (e) => {
        const value = e.currentTarget.value;
        if (!this.isValid(value))
            return;

        this.setState({to: !value ? value : parseInt(value)});
        this.setUpdate();
    }
    setUpdate = () => {
        this.setDirty();
        this.setTimer();
    }
    isValid = (value) => regexp.test(value) || !value;
    render() {
        return (
            <Container>
            <Form action="" method="post" className="form-horizontal">
                <FormGroup className="facet_numberrange" row>
                    <Col sm="6">
                        <span className="help-block text-center font-weight-bold text-muted text-uppercase small">От</span>
                        <input
                            className={classNames("form-control")}
                            name="from"
                            type="numer"
                            onChange={this.setFrom}
                            value={this.state.from} 
                            placeholder={this.props.attribute.from}/>
                        
                    </Col>
                    <Col sm="6">
                        <span className="help-block text-center font-weight-bold text-muted text-uppercase small">До</span>
                        <input
                            onChange={this.setTo} 
                            className={classNames("form-control")}
                            name="to"
                            type="numer"
                            value={this.state.to}
                            placeholder={this.props.attribute.to}/> 
                    </Col>
                </FormGroup>
            </Form>
            </Container>
        );
    }
}

export default NumberRange;