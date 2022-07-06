import React from 'react';
import './WizardStep.scss';
import className from 'classnames';

class WizardStep extends React.Component {
    constructor(props) {
        super(props);
        this.steps = {
            'category': { index: 0, title: 'Что будем размещать?'},
            'options' : { index: 1, title: 'Параметры'},
            'photos'  : { index: 2, title: 'Фото'}
        };
    }
    setCategoryStepHandler = (e) => {
        e.preventDefault();
        e.stopPropagation();
        this.props.setCategoryStep();
    }
    setOptionStepHandler = (e) => {
        e.preventDefault();
        e.stopPropagation();
        this.props.setOptionStep();
    }
    setPhotoStepHandler = (e) => {
        e.preventDefault();
        e.stopPropagation();
        this.props.setPhotoStep();
    }
    render() {
        const current = this.steps[this.props.step];
        return (
            <React.Fragment>
            <h1 className="display-4">{current.title}</h1>
            <div className="wizard">
                <div className="wizard-inner">
                    <div className="connecting-line"></div>
                    <ul className="nav nav-tabs" role="tablist">
                        <li role="presentation" className="nav-item">
                            <a href="#step1" 
                                onClick={this.setCategoryStepHandler}
                                data-toggle="tab" 
                                aria-controls="step1"
                                role="tab"
                                title="Категория" 
                                className={className("nav-link", 
                                    { "active": this.props.step === 'category' },
                                    { "done": current.index > this.steps['category'].index })}>
                                <span className="round-tab">
                                    <i className="fa fa-list"></i>
                                </span>
                            </a>
                        </li>
                        <li role="presentation" className="nav-item">
                            <a href="#step2" 
                                onClick={this.setOptionStepHandler}
                                data-toggle="tab" 
                                aria-controls="step2" 
                                role="tab" 
                                title="Параметры" 
                                className={className("nav-link", 
                                    { 'active': this.props.step === 'options' },
                                    { 'disabled': current.index < this.steps['options'].index },
                                    { "done": current.index > this.steps['options'].index })}>
                                <span className="round-tab">
                                    <i className="fa fa-info"></i>
                                </span>
                            </a>
                        </li>
                        <li role="presentation" className="nav-item">
                            <a href="#step3" 
                                onClick={this.setPhotoStepHandler}
                                data-toggle="tab" 
                                aria-controls="step3" 
                                role="tab" 
                                title="Фото" 
                                className={className("nav-link", 
                                    { "active": this.props.step === 'photos' },
                                    { 'disabled': current.index < this.steps['photos'].index },
                                    { "done": current.index > this.steps['photos'].index })}>
                                <span className="round-tab">
                                    <i className="fa fa-photo"></i>
                                </span>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
            </React.Fragment>
        );
    }
}

export default WizardStep;