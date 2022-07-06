import React from 'react';
import MaterialTable from 'material-table';

export default function CategoryGrid(props) {
    const roots = props.categories.reduce((result, item) => {
        result[item.key] = item.name;
        return result;
    }, {});

    const columns = [
        { title: 'Name', field: 'name' },
        { title: 'Parent', field: 'parent_key', lookup: roots },
        { title: 'Modified Date', field: 'modified_date', type: 'datetime', editable: 'never' }
    ];

    const onRowUpdate = (newCategory, _) => {
        return new Promise((resolve, reject) => {
            if (newCategory.parent_key && newCategory.parent_key === newCategory.key)
                reject();
            props.onUpdate(newCategory);
            resolve();
        });
    }
    const onRowAdd = (newCategory) => {
        return new Promise((resolve, _) => {
            props.onCreate(newCategory);
            resolve();
        });
    }
    const onRowDelete = (oldCategory) => {
        return new Promise((resolve, _) => {
            props.onDelete(oldCategory.key);
            resolve();
        });
    }
    return (
        <MaterialTable
            title="Manage"
            columns={columns}
            data={props.categories.map(entry => { entry.parent_key = entry.parent_key || ''; return entry; })}
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