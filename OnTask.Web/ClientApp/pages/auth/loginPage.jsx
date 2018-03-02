/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

/**
 * Internal dependencies
 */
import * as authActions from 'ClientApp/actions/authActions';
import PasswordInput from 'ClientApp/components/common/passwordInput';
import TextInput from 'ClientApp/components/common/textInput';

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
            <div>
                <h1>Login</h1>
                <form>
                    <TextInput
                        name="username"
                        label="Email"
                        disabled={loggingIn}
                        value={this.state.username}
                        onChange={this.handleChange}
                        error={this.props.errors.email}
                    />
                    <PasswordInput
                        name="password"
                        label="Password"
                        disabled={loggingIn}
                        value={this.state.password}
                        onChange={this.handleChange}
                        error={this.props.errors.password}
                    />
                    <input
                        type="submit"
                        disabled={!this.canSubmitForm()}
                        value={loggingIn ? 'Logging In...' : 'Login'}
                        className="btn btn-primary"
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
