import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';
import { Container } from 'reactstrap';
import { CategoryStep, OptionStep, ImageStep } from '../Interaction';
import { setPhotoStep, setCategoryStep, setOptionStep, reset } from '../../../../../store/actions/wizard';
import { WizardStep } from '../Steps';
import './FormControl.scss';

class FormControl extends Component {
    constructor(props) {
        super(props);
        this.steps = {
            'category': (props) => { return <CategoryStep {...props}/> },
            'options': (props) => { return <OptionStep {...props}/> },
            'photos': (props) => { return <ImageStep {...props}/> }
        }
    }
    componentWillUnmount() {
        this.props.resetWizard();
    }
    render() {
        if (this.props.module_completed)
            return <Redirect to="/"/>
        return (
            <Container>
                <WizardStep 
                    step={this.props.step}
                    {...this.props}/>
                {this.steps[this.props.step](this.props)}
            </Container>
        );
    }
}

const mapping = (state) => {
    return {
        step: state.wizard.step,
        module_completed: state.wizard.module_completed
    }
}

const dispatchers = dispatch => {
    return {
		setCategoryStep: () => dispatch(setCategoryStep()),
        setOptionStep: () => dispatch(setOptionStep()),
        setPhotoStep: () => dispatch(setPhotoStep()),
        resetWizard: () => dispatch(reset())
    }
}

export default connect(mapping, dispatchers)(FormControl);