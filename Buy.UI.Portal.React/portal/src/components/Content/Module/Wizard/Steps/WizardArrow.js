import React from 'react';
import { Container, Row } from 'reactstrap';
import className from 'classnames';
import './WizardArrow.scss';

class WizardArrow extends React.Component  {
    render() {
        return (
            <Container className="col-md-12">
                <Row>
                    <div id="arrow_2" className="col-md-12 arrow-wrapper arrow-wrapper">
                        <div onClick={this.props.prevStep} className={className("arrow arrow--left", {"disabled": !this.props.prevStep})}>
                            <span>Назад</span>
                        </div>

                        <div className="block"><h1>ШАГ {this.props.step}</h1></div>

                        <div onClick={this.props.nextStep} className={className("arrow arrow--right", {"disabled": !this.props.nextStep})}>
                            <span>Далее</span>
                        </div>
                    </div>
                </Row>
            </Container>
        );
    }
}

export default WizardArrow;