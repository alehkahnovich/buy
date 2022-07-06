import React, { Component } from 'react';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import thunk from 'redux-thunk';
// import { renderRoutes } from 'react-router-config';
import './App.scss';
import { Provider } from 'react-redux';
import { createStore, combineReducers, applyMiddleware } from 'redux';
import bucketsReducer from './store/reducers/buckets';
import contentReducer from './store/reducers/content';
import queryReducer from './store/reducers/query';
import wizardReducer from './store/reducers/wizard';

const store = createStore(combineReducers({
    buckets: bucketsReducer,
    content: contentReducer,
    query: queryReducer,
    wizard: wizardReducer
}), applyMiddleware(thunk));

const loading = () => <div className="animated fadeIn pt-3 text-center">Loading...</div>;

// Containers
const DefaultLayout = React.lazy(() => import('./containers/DefaultLayout'));
const ContentLayout = React.lazy(() => import('./containers/ContentLayout'));

// Pages
const Login = React.lazy(() => import('./views/Pages/Login'));
const Register = React.lazy(() => import('./views/Pages/Register'));
const Page404 = React.lazy(() => import('./views/Pages/Page404'));
const Page500 = React.lazy(() => import('./views/Pages/Page500'));

class App extends Component {

  render() {
    return (
      <Provider store={store}>
        <BrowserRouter>
            <React.Suspense fallback={loading()}>
              <Switch>
                <Route exact path="/login" name="Login Page" render={props => <Login {...props}/>} />
                <Route exact path="/register" name="Register Page" render={props => <Register {...props}/>} />
                <Route exact path="/404" name="Page 404" render={props => <Page404 {...props}/>} />
                <Route exact path="/500" name="Page 500" render={props => <Page500 {...props}/>} />
                <Route path="/content" name="Размещение" render={props => <ContentLayout {...props}/>} />
                <Route path="/" name="Объявления" render={props => <DefaultLayout {...props}/>} />
              </Switch>
            </React.Suspense>
        </BrowserRouter>
      </Provider>
    );
  }
}

export default App;
