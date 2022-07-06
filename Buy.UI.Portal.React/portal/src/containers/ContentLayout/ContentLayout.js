import React, { Component, Suspense } from 'react';
import { Switch, Route } from 'react-router-dom';
import Loader from '../../components/Common/Loader';
import {
    AppFooter,
    AppHeader
  } from '@coreui/react';
import './ContentLayout.scss';

const DefaultFooter = React.lazy(() => import('../DefaultLayout/DefaultFooter'));
const DefaultHeader = React.lazy(() => import('../DefaultLayout/DefaultHeader'));
const Content = React.lazy(() => import('../../components/Content/Module'));

class ContentLayout extends Component {
    loading = () => <div className="text-center"><Loader /></div>
    render () {
        return (
            <div className="app">
                <AppHeader fixed>
                <Suspense  fallback={this.loading()}>
                    <DefaultHeader onLogout={e=>this.signOut(e)}/>
                </Suspense>
                </AppHeader>
                <div className="app-body">
                    <Switch>
                        <Route path="/content" name="Размещение" render={props => <Content {...this.props} {...props}/>} />
                    </Switch>
                </div>
                <AppFooter className="content_footer">
                    <Suspense fallback={this.loading()}>
                        <DefaultFooter />
                    </Suspense>
                </AppFooter>
            </div>
        );
    }
}

export default ContentLayout;