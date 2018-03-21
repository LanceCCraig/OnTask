/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import TextField from 'material-ui/TextField';
import RaisedButton from 'material-ui/RaisedButton';


/**
 * Internal dependencies
 */
import * as authActions from 'ClientApp/actions/authActions';

class LoginPage extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            username: '',
            password: ''
        }
    }

    submitForm = e => {
        e.preventDefault();
        this.props.actions.login(this.state.username, this.state.password);
    }

    handleChange = e => {
        const { name, value } = e.currentTarget;
        this.setState({ [name]: value });
    }

    hasLoginDetails() {
        if (this.state.username === '' || this.state.password === '') {
            return false;
        }
        return true;
    }

    canSubmitForm = () => {
        if (this.props.loggingIn) {
            return false;
        }
        return this.hasLoginDetails();
    }

    render() {
        const { loggingIn } = this.props;
        return (
            <div style={{textAlign:'center'}} >
                <h1>Login</h1>
                <form>
                    <TextField
                        name="username"
                        floatingLabelText="Email"
                        disabled={loggingIn}
                        value={this.state.username}
                        onChange={this.handleChange}
                        errorText={this.props.errors.email}
                    /><br/>
                    <TextField
                        name="password"
                        type="password"
                        floatingLabelText="Password"
                        disabled={loggingIn}
                        value={this.state.password}
                        onChange={this.handleChange}
                        errorText={this.props.errors.password}
                    /><br/>
                    <RaisedButton
                        disabled={!this.canSubmitForm()}
                        primary
                        label={loggingIn ? 'Logging In...' : 'Login'}
                        onClick={this.submitForm}
                    />
                </form>
            </div>
        );
    }
}

LoginPage.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {
    return {
        loggingIn: state.auth.loggingIn,
        errors: state.auth.errors
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(authActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(LoginPage);
