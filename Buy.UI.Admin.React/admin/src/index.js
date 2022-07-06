import ReactDOM from 'react-dom';
import React from 'react';
import thunk from 'redux-thunk';
import { Provider } from 'react-redux';
import { createStore, combineReducers, applyMiddleware } from 'redux';
import { BrowserRouter } from 'react-router-dom';
import categoryReducer from './store/reducers/category';
import proeprtyReducer from './store/reducers/property';
import Dashboard from './components/Dashboard';

const store = createStore(combineReducers({
    category: categoryReducer,
    property: proeprtyReducer
}), applyMiddleware(thunk));

ReactDOM.render(
    <Provider store={store}>
        <BrowserRouter>
            <Dashboard />
        </BrowserRouter>
    </Provider>, 
document.getElementById('root'));