import React, { Component, Suspense } from 'react';
import { Switch, Route } from 'react-router-dom';
import {
  AppFooter,
  AppHeader
} from '@coreui/react';


const DefaultFooter = React.lazy(() => import('./DefaultFooter'));
const DefaultBody = React.lazy(() => import('./DefaultBody'));
const DefaultHeader = React.lazy(() => import('./DefaultHeader'));

class DefaultLayout extends Component {

  loading = () => <div className="animated fadeIn pt-1 text-center">Loading...</div>

  signOut(e) {
    e.preventDefault()
    this.props.history.push('/login')
  }

  render() {
    return (
      <div className="app">
        <AppHeader fixed>
          <Suspense  fallback={this.loading()}>
            <DefaultHeader onLogout={e=>this.signOut(e)}/>
          </Suspense>
        </AppHeader>
        <div className="app-body">
          <Switch>
            <Route path="/category/:cat" name="Категории" render={props => <DefaultBody {...this.props} {...props}/>} />
            <Route path="/" name="Объявления" render={props => <DefaultBody {...this.props} {...props}/>} />
          </Switch>
        </div>
        <AppFooter>
          <Suspense fallback={this.loading()}>
            <DefaultFooter />
          </Suspense>
        </AppFooter>
      </div>
    );
  }
}

export default DefaultLayout;
