import React from 'react';
class Select extends React.Component {
    setOption = (event) => {
        this.props.onChange({
            id: this.props.attribute.id,
            behavior: this.props.attribute.behavior,
            option: {
                id: this.props.attribute.id,
                value: event.currentTarget.value
            }
        });
    }
    render() {
        const options = this.props.attribute.options.map(entry => {
            return (<option key={entry.id} value={entry.id}>{entry.name}</option>)
        });
        return (
            <select onChange={this.setOption} className="form-control">
                <option defaultValue data-id={''}>Выберите...</option>
                {options}
            </select>
        );
    }
}

export default Select;