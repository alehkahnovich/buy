import React from 'react';
import { setProperty, setName, setDescription, deleteProperty, receiveOptions, setPhotoStep, setCategoryStep } from '../../../../../store/actions/wizard';
import { connect } from 'react-redux';
import Controls from '../Controls';
import { WizardArrow } from '../Steps';
import Loader from '../../../../Common/Loader';

class OptionStep extends React.Component {
    componentDidMount() {
        this.props.receiveOptions(this.props.module.cat);
    }
    nextStep = () => {
        this.props.setPhotoStep();
    }
    prevStep = () => {
        this.props.setCategoryStep();
    }
    setNameHandler = (event) => {
        this.props.setName(event.currentTarget.value);
    }
    setDescriptionHandler = (event) => {
        this.props.setDescription(event.currentTarget.value);
    }
    getContent = () => {
        if (this.props.attributes.length === 0)
            return <div className="col-sm-12"><div className="text-center"><Loader /></div></div>;
        
        return (
            <React.Fragment>
                <div className="col-sm-12">
                    <div className="col-md-12 mb-3">
                        <span className="help-block text-center font-weight-bold text-muted text-uppercase">Название</span>
                        <input type="text" className="form-control" placeholder="Название" value={this.props.module.name || ''} onChange={this.setNameHandler} />
                    </div>
                    <div className="col-md-12 mb-3">
                        <span className="help-block text-center font-weight-bold text-muted text-uppercase">Краткое описание</span>
                        <textarea className="form-control" value={this.props.module.description} onChange={this.setDescriptionHandler} placeholder="Краткое описание..." >
                        </textarea>
                    </div>
                </div>
                <div className="col-sm-12">
                    <Controls removeProperty={this.props.deleteProperty} setProperty={this.props.setProperty} attributes={this.props.attributes}/>
                </div>
            </React.Fragment>
        );
    }
    render() {
        return (
            <div>
                <div className="container col-md-8 col-md-offset-4 content_form">
                    <div className="row">
                        {this.getContent()}
                    </div>
                </div>
                <WizardArrow step={2} nextStep={this.nextStep} prevStep={this.prevStep} />
            </div>
        );
    }
}


const mapping = (state) => {
    return {
        categories: state.buckets.roots,
        category: state.wizard.category,
        attributes: state.wizard.attributes,
        module: state.wizard.module
    }
}

const dispatchers = dispatch => {
    return {
        setName: (name) => dispatch(setName(name)),
        setDescription: (description) => dispatch(setDescription(description)),
        setProperty: (property) => dispatch(setProperty(property)),
        deleteProperty: (property) => dispatch(deleteProperty(property)),
        receiveOptions: (cat) => dispatch(receiveOptions(cat)),
        setPhotoStep: () => dispatch(setPhotoStep()),
        setCategoryStep: () => dispatch(setCategoryStep())
    }
}

export default connect(mapping, dispatchers)(OptionStep);