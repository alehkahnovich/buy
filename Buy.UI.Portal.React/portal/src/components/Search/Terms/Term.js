import React, { Component } from 'react';
import { Row, Col } from 'reactstrap';
import './Term.scss'
class Term extends Component {
    constructor(props) {
        super(props);
        this.state = {
            term: ''
        };
    }
    onKeyPress = (event) => {
        if (event.key !== 'Enter') return;
        this.submit();
    }
    submit = () => {
        this.props.onTermSet(this.state.term);
    }
    render() {
        return (
        <Row className="term_holder">
            <Col>
                <div className="input-group input-group-lg">
                    <div className="input-group-prepend">
                        <span className="input-group-text">
                            <i className="fa fa-search fa-lg"></i>
                        </span>
                    </div>
                    <input placeholder="Поиск объявлений..."
                        onKeyPress={this.onKeyPress}
                        onChange={(event) => this.setState({
                            term: event.currentTarget.value
                        })}
                        value={this.state.term} 
                        type="text" 
                        className="form-control" />
                </div>
            </Col>
        </Row>
        );
    }
}

export default Term;