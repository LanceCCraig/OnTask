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

class RegisterPage extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            username: '',
            password: '',
            confirmPassword: ''
        }
    }

    submitForm = e => {
        e.preventDefault();
        this.props.actions.register(this.state.username, this.state.password, this.state.confirmPassword).then(() => {
            // Reload all user data into the store.
            // getAuthToken;
        });
    }

    handleChange = e => {
        const { name, value } = e.currentTarget;
        this.setState({ [name]: value });
    }

    hasRegisterDetails() {
        if (this.state.username === '' || 
            this.state.password === '' ||
            this.state.confirmPassword == '') {
            return false;
        }
        return true;
    }

    canSubmitForm = () => {
        if (this.props.registering) {
            return false;
        }
        return this.hasRegisterDetails();
    }

    render() {
        const { registering } = this.props;
        return (
            <div style={{textAlign:'center'}} >
                <h1>Register</h1>
                <form>
                    <TextField
                        name="username"
                        floatingLabelText="Email"
                        disabled={registering}
                        value={this.state.username}
                        onChange={this.handleChange}
                        errorText={this.props.errors.email}
                    /><br/>
                    <TextField
                        name="password"
                        type="password"
                        floatingLabelText="Password"
                        disabled={registering}
                        value={this.state.password}
                        onChange={this.handleChange}
                        errorText={this.props.errors.password}
                    /><br/>
                    <TextField
                        name="confirmPassword"
                        type="password"
                        floatingLabelText="Confirm Password"
                        disabled={registering}
                        value={this.state.confirmPassword}
                        onChange={this.handleChange}
                        errorText={this.props.errors.confirmPassword}
                    /><br/>
                    <RaisedButton
                        type="submit"
                        disabled={!this.canSubmitForm()}
                        primary
                        label={registering ? 'Registering...' : 'Register'}
                        onClick={this.submitForm}
                    />
                </form>
            </div>
        );
    }
}

RegisterPage.propTypes = {
    actions: PropTypes.object.isRequired
}

function mapStateToProps(state, ownProps) {
    return {
        registering: state.auth.registering,
        errors: state.auth.errors
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(authActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(RegisterPage);
