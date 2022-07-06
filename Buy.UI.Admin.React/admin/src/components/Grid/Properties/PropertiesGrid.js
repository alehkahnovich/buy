import React from 'react';
import MaterialTable from 'material-table';

const types = {
    'string': 'string',
    'number': 'number',
    'datetime': 'datetime',
    'boolean': 'boolean',
    'money': 'money'
};

const facets = {
    'true': 'true',
    'false': 'false'
}

export default function PropertiesGrid(props) {
    const roots = props.properties.reduce((result, item) => {
        result[item.key] = item.name;
        return result;
    }, {});

    const columns = [
        { title: 'Name', field: 'name' },
        { title: 'Type', field: 'type', lookup: types },
        { title: 'Facet', field: 'is_facet', lookup: facets },
        { title: 'Parent', field: 'parent_key', lookup: roots },
        { title: 'Modified Date', field: 'modified_date', type: 'datetime', editable: 'never' }
    ];

    const onRowUpdate = (newProperty, _) => {
        return new Promise((resolve, reject) => {
            if (newProperty.parent_key && newProperty.parent_key === newProperty.key)
                reject();
            props.onUpdate(newProperty);
            resolve();
        });
    }
    const onRowAdd = (newProperty) => {
        return new Promise((resolve, _) => {
            props.onCreate(newProperty);
            resolve();
        });
    }
    const onRowDelete = (oldProperty) => {
        return new Promise((resolve, _) => {
            props.onDelete(oldProperty.key);
            resolve();
        });
    }
    return (
        <MaterialTable
            title="Manage"
            columns={columns}
            data={props.properties.map(entry => { entry.parent_key = entry.parent_key || ''; return entry; })}
            isLoading={props.isLoading}
            options={{actionsColumnIndex: -1}}
            editable={{
                isDeletable: data => !data.with_siblings,
                onRowAdd: onRowAdd,
                onRowUpdate: onRowUpdate,
                onRowDelete: onRowDelete
            }}>
        </MaterialTable>
    );
}