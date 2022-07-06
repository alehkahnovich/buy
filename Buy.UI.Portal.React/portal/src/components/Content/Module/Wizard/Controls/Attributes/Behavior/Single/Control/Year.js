import React from 'react';
class Year extends React.Component {
    setYear = (event) => {
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
        const year = new Date().getFullYear() + 1;
        const start = year - 40;
        const options = [];
        for (let index = start; index < year; index++) {
            options.push(<option key={index} value={index}>{index}</option>);
        }

        return (
            <select onChange={this.setYear} className="form-control">
                {options}
            </select>
        );
    }
}

export default Year;