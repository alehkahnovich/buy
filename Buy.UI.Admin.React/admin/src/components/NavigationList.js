import React from 'react';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import DashboardIcon from '@material-ui/icons/Dashboard';
import CategoryIcon from '@material-ui/icons/List';
import LayersIcon from '@material-ui/icons/Layers';
import { NavLink } from 'react-router-dom';
import './NavigationList.scss';

export default function NavigationList(props) {
    return (
    <div>
        <NavLink className="Navigation_Item" exact to='/' activeClassName={"active"}>
            <ListItem button>
                    <ListItemIcon>
                        <DashboardIcon />
                    </ListItemIcon>
                    <ListItemText primary="Dasboard" />
            </ListItem>
        </NavLink>
        <NavLink className="Navigation_Item" exact to='/categories' activeClassName={"active"}>
            <ListItem button>
                <ListItemIcon>
                    <CategoryIcon />
                </ListItemIcon>
                <ListItemText primary="Categories" />
            </ListItem>
        </NavLink>
        <NavLink className="Navigation_Item" exact to='/properties' activeClassName={"active"}>
            <ListItem button>
                <ListItemIcon>
                    <LayersIcon />
                </ListItemIcon>
                <ListItemText primary="Properties" />
            </ListItem>
        </NavLink>
    </div>
    );
}