import React, { Component, Suspense } from 'react';
// routes config
import routes from '../../routes';
import * as router from 'react-router-dom';
import { Container } from 'reactstrap';
import { connect } from 'react-redux';
import { receiveRoots, toogleBucketsMenu } from '../../store/actions/buckets';
import { receiveContent } from '../../store/actions/content';
import { setTerm, setFacet, setCat, acceptQuery } from '../../store/actions/query';
import MenuMain from '../../components/Search/Menu';
import Content from '../../components/Search/Content';
import Filters from '../../components/Search/Filters';
import Term from '../../components/Search/Terms';
import Loader from '../../components/Common/Loader';
import {
  AppAside,
  AppBreadcrumb2 as AppBreadcrumb
} from '@coreui/react';

const DefaultAside = React.lazy(() => import('./DefaultAside'));

class DefaultBody extends Component {
	onFacetSet = (facet, action) => this.props.setFacet(facet, action.type);
	onTermSet = (term) => this.props.setTerm(term);
    loading = () => <div className="text-center"><Loader /></div>
	componentDidMount() {
		this.props.receiveBuckets();
		if (this.props.match.params.cat) {
			this.props.setCat(this.props.match.params.cat);
			return;
		}

		this.props.receiveContent(this.props.query);
	}
	componentDidUpdate(prevProps) {
		if (this.props.match.params.cat !== prevProps.match.params.cat) {
			this.props.setCat(this.props.match.params.cat);
		}

		if (this.props.query.__isDirty) {
			this.props.acceptQuery();
			this.props.receiveContent(this.props.query);
		}
	}
    render() {
		const side = this.props.match.params.cat && this.props.content.facets
		? <Filters loading={this.props.contentLoading} facets={this.props.content.facets} total={this.props.content.total} onFacetSet={this.onFacetSet}/>
		: <DefaultAside />
	
        return (
			<React.Fragment>
				<MenuMain {...this.props}/>
				<main className="main">
					<AppBreadcrumb appRoutes={routes} router={router}/>
					<Container fluid>
						<Suspense fallback={this.loading()}>
							<Term onTermSet={this.onTermSet}/>
							<Content content={this.props.content} loading={this.props.contentLoading}/>
						</Suspense>
					</Container>
					</main>
					<AppAside fixed display="lg">
						<Suspense fallback={this.loading()}>
							{side}
						</Suspense>
					</AppAside>
			</React.Fragment>
        );
    }
}


const mapping = (state) => {
    return {
		categories: state.buckets.roots,
		content: state.content.searchResult,
		contentLoading: state.content.loading,
		query: state.query,
		menuOpen: state.buckets.open
    }
}

const dispatchers = dispatch => {
    return {
		receiveBuckets: () => dispatch(receiveRoots()),
		receiveContent: (query) => dispatch(receiveContent(query)),
		setTerm: (term) => dispatch(setTerm(term)),
		setFacet: (facet, type) => dispatch(setFacet(facet, type)),
		setCat: (cat) => dispatch(setCat(cat)),
		acceptQuery: () => dispatch(acceptQuery()),
		toogleBucketsMenu: () => dispatch(toogleBucketsMenu())
    }
}

export default connect(mapping, dispatchers)(DefaultBody);