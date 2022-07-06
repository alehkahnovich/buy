import React from 'react';
import 'bootstrap/dist/css/bootstrap.css';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import { Provider } from 'react-redux';
import { createStore, combineReducers, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import contentReducer from './store/reducers/contentReducer';
import CommonLoader from './components/Loader';
//import * as signalR from "@microsoft/signalr";
import './App.css';

const ChoiceLayout = React.lazy(() => import('./containers/Layout/ChoiceLayout'));
const PushLayout = React.lazy(() => import('./containers/Layout/PushLayout'));
const PullLayout = React.lazy(() => import('./containers/Layout/PullLayout'));

const store = createStore(combineReducers({
  buckets: contentReducer
}), applyMiddleware(thunk));

function App() {
  //const [ term, setTerm ] = useState('');

  // const connection = new signalR.HubConnectionBuilder()
  //   .withUrl("http://localhost:5105/push")
  //   .configureLogging(signalR.LogLevel.Information)
  //   .withAutomaticReconnect()
  //   .build();

  // connection.start().then(function () {
  //     console.log("connected");
  // });

  // connection.on('PullAsync', (message) => {
  //   console.log('received', message);
  // })

  // const setPush = (e) => {
  //   setTerm(e.currentTarget.value);
  //   connection.invoke('Push', {
  //     Category: 1,
  //     Description: e.currentTarget.value
  //   }).catch(err => console.error(err.toString()));
  // }

  return (
    <Provider store={store}>
      <BrowserRouter>
        <React.Suspense fallback={<CommonLoader />}>
          <Switch>
            <Route exact path='/push' render={props => <PushLayout {...props}/>}/>
            <Route path='/pull/:bucket?' render={props => <PullLayout {...props}/>}/>
            <Route exact path='/' render={props => <ChoiceLayout {...props}/>}/>
          </Switch>
        </React.Suspense>
      </BrowserRouter>
    </Provider>
  );
}

export default App;
