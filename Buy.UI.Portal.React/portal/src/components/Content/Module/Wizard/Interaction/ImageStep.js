import React from 'react';
import { connect } from 'react-redux';
import className from 'classnames';
import DragAndDrop from '../DragDrop';
import { Col } from 'reactstrap';
import { setPhotos, setOptionStep, createModule } from '../../../../../store/actions/wizard';
import Carousel from '../Controls/Carousel';
import { WizardArrow } from '../Steps';
import Loader from '../../../../Common/Loader';
import './ImageStep.scss';

class ImageStep extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            files: [],
            indetifier: 0
        };
    }
    submitModuleHandler = () => {
        this.props.createModule(this.props.module);
    }
    uploadPhotosHandler = () => {
        var data = new FormData();
        for(let index = 0; index < this.state.files.length; index++) {
            data.append(`photo_${index}`, this.state.files[index]);
        }
        this.props.setPhotos(data);
        this.setState({
            files: []
        });
    }
    handleDropHandler = (files) => {
        const mutated = [...this.state.files];
        let identity = this.state.indetifier;
        for (let index = 0; index < files.length; index++) {
            identity++;
            const current = files[index];
            current.id = identity;
            mutated.push(current);
        }

        this.setState({
            files: mutated,
            indetifier: identity
        });
    }
    deleteFileHandler = (file) => {
        const files = [...this.state.files];
        const index = files.findIndex(entry => entry.id === file.id);
        files.splice(index, 1);
        this.setState({
            files: files
        });
    }
    getLayout = () => {
        const carousel = this.props.artifacts.length > 0
        ? <Carousel items={this.props.artifacts} />
        : null;

        const files = this.state.files.map((entry, index) => {
            return (<li className="file_list_item" key={index}>
                <span className="text-uppercase">{entry.name}</span>
                <span onClick={() => this.deleteFileHandler(entry)} className="badge badge-light file_control">
                    <i className="fa fa-close"></i>
                </span>
            </li>);
        });

        const uploads = this.props.loading
        ? (<div className="text-center"><Loader /></div>)
        : (<DragAndDrop handleDrop={this.handleDropHandler}>
                <ul>
                    {files}
                </ul>
            </DragAndDrop>);

        return (
            <React.Fragment>
                {carousel}
                {uploads}
            </React.Fragment>
        );
    }
    getBtnControls = () => {
        const controls = [];
        const title = this.props.artifacts.length === 0 ? 'Загрузить' : 'Добавить фото';
        if (this.state.files.length > 0)
            controls.push(<div key={'uploadPhotosBtn'}  className={className("col-md-6 mb-3", { 'offset-md-3': this.props.artifacts.length === 0})}>
                <button onClick={this.uploadPhotosHandler} className="btn btn-square btn-block btn-primary" type="button">{title}</button>
            </div>);

        if (this.props.artifacts.length > 0)
            controls.push(<div key={'pushBtn'} className={className("col-md-6 mb-3", { 'offset-md-3': this.state.files.length === 0})}>
                <button onClick={this.submitModuleHandler} className="btn btn-square btn-block btn-success" type="button">Разместить</button>
            </div>);

        return (<div className="col-md-12 mb-3"><div className="row">{controls}</div></div>);
    }
    render () {       
        const control = this.getLayout();
        const uploadBtn = this.getBtnControls();

        return(
            <div> 
                <Col sm={12}>
                    <div className="col-md-12 mb-3">
                        {control}
                    </div>
                    {uploadBtn}
                </Col>
                <WizardArrow step={3} prevStep={this.props.setOptionStep} />
            </div>
        );
    }
}

const mapping = (state) => {
    return {
        module: state.wizard.module,
        artifacts: state.wizard.artifacts,
        loading: state.wizard.loading
    }
}

const dispatchers = dispatch => {
    return {
        setPhotos: (payload) => dispatch(setPhotos(payload)),
        setOptionStep: () => dispatch(setOptionStep()),
        createModule: (entry) => dispatch(createModule(entry))
    }
}


export default connect(mapping, dispatchers)(ImageStep);