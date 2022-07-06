import React from 'react';
import { receiveRoots } from '../../../../../store/actions/buckets';
import { setCategory, setOptionStep } from '../../../../../store/actions/wizard';
import Loader from '../../../../Common/Loader';
import { connect } from 'react-redux';
import { Container, Row, Col } from 'reactstrap';
import { WizardArrow } from '../Steps';

class CategoryStep extends React.Component {
    componentDidMount() {
        if (this.props.categories.length === 0)
            this.props.receiveBuckets();
    }
    setCategory = (event) => {
        let result = '';
        
        for(let index = 0; index < this.props.categories.length; index++) {
            const found = this.findCategory(event.target.value, this.props.categories[index]);
            if (found) {
                result = found.id;
                break;
            }
        }

        this.props.setCategory(result);
    }
    nextStep = () => {
        this.props.setOptionStep();
    }
    findCategory = (name, category) => {
        if (category.name === name) return category;
        for (let index = 0; index < category.siblings.length; index++) {
            const found = this.findCategory(name, category.siblings[index]);
            if (found) return found;
        }
        return null;
    }
    render() {
        const options = this.props.categories.map(category => {
            const siblings = category.siblings.map(sibling => {
                return (<option key={sibling.id} data-id={sibling.id}>{sibling.name}</option>);
            });

            return (
                <optgroup key={category.id} data-id={category.id} label={category.name}>
                    {siblings}
                </optgroup>
            );
        });

        const control = this.props.categories.length === 0
        ? <div className="text-center"><Loader /></div>
        : (
            <React.Fragment>
                <span className="help-block text-center font-weight-bold text-muted text-uppercase">Категория</span>
                <select onChange={this.setCategory} className="form-control" placeholder="Выберите категорию">
                    <option defaultValue data-id={''}>-</option>
                    {options}
                </select>
            </React.Fragment>
        );

        return (
            <div>
                <Container className="col-md-8 col-md-offset-2 content_form">
                    <Row>
                        <Col sm={12}>
                            <div className="col-md-12 mb-3">
                                {control}
                            </div>
                        </Col>
                    </Row>
                </Container>
                <WizardArrow step={1} nextStep={this.nextStep}/>
            </div>
        );
    }
}


const mapping = (state) => {
    return {
        categories: state.buckets.roots,
        category: state.wizard.category,
        module: state.wizard.module
    }
}

const dispatchers = dispatch => {
    return {
		receiveBuckets: () => dispatch(receiveRoots()),
        setCategory: (category) => dispatch(setCategory(category)),
        setOptionStep: () => dispatch(setOptionStep())
    }
}

export default connect(mapping, dispatchers)(CategoryStep);