import React from 'react';
import className from 'classnames';
import { Container, CardBody } from 'reactstrap';
import './DragAndDrop.scss';

class DragAndDrop extends React.Component {
    constructor(props) {
        super(props);
        this.dropRef = React.createRef();
        this.uploadRef = React.createRef();
        this.dragCounter = 0;
        this.state = {
            dragging: false
        }
    }
    handleDrag = (e) => {
        e.preventDefault();
        e.stopPropagation();
    }
    handleDragIn = (e) => {
        e.preventDefault();
        e.stopPropagation();
        this.dragCounter++;
        if (e.dataTransfer.items && e.dataTransfer.items.length > 0) {
            this.setState({dragging: true})
        }
    }
    handleDragOut = (e) => {
        e.preventDefault();
        e.stopPropagation();
        this.dragCounter--;
        if (this.dragCounter > 0) return;
        this.setState({dragging: false})
    }
    handleDrop = (e) => {
        e.preventDefault();
        e.stopPropagation();
        this.setState({dragging: false})
        if (e.dataTransfer.files && e.dataTransfer.files.length > 0) {
            this.props.handleDrop(e.dataTransfer.files);
            e.dataTransfer.clearData();
            this.dragCounter = 0;
        }
    }
    componentDidMount() {
        const div = this.dropRef.current;
        div.addEventListener('dragenter', this.handleDragIn);
        div.addEventListener('dragleave', this.handleDragOut);
        div.addEventListener('dragover', this.handleDrag);
        div.addEventListener('drop', this.handleDrop);
    }
    componentWillUnmount() {
        const div = this.dropRef.current;
        div.removeEventListener('dragenter', this.handleDragIn);
        div.removeEventListener('dragleave', this.handleDragOut);
        div.removeEventListener('dragover', this.handleDrag);
        div.removeEventListener('drop', this.handleDrop);
    }
    uploadFileHandler = (e) => {
        this.props.handleDrop(e.target.files);
    }
    render () {
        return (
        <Container className={className('upload_container', {'upload-dragging': this.state.dragging})}>
            <CardBody>
                <h1>Перетащите файлы сюда</h1>
                <div ref={this.dropRef}>
                    <div className="text-center">
                        <input onChange={this.uploadFileHandler} className="upload_input" type="file" ref={this.uploadRef} multiple/>
                        <i onClick={() => this.uploadRef.current.click()} className="fa fa-cloud-upload fa-lg upload_icon"></i>
                    </div>
                </div>
                {this.props.children}
            </CardBody>
        </Container>);
    }
}

export default DragAndDrop;