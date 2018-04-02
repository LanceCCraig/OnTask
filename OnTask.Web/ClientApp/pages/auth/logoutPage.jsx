/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { routerActions } from 'react-router-redux';

/**
 * Internal dependencies
 */
import * as authActions from 'ClientApp/actions/authActions';

class LogoutPage extends React.Component {
    componentWillMount() {
        this.props.actions.logout();
        this.props.routerActions.push('/');
    }

    render() {
        return null;
    }
}

LogoutPage.propTypes = {
    actions: PropTypes.object.isRequired,
    routerActions: PropTypes.object.isRequired
};

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(authActions, dispatch),
        routerActions: bindActionCreators(routerActions, dispatch)
    };
}

export default connect(null, mapDispatchToProps)(LogoutPage);
