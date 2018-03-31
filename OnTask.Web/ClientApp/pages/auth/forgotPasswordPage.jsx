import React from 'react';
import PropTypes from 'prop-types';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import TextField from 'material-ui/TextField';
import RaisedButton from 'material-ui/RaisedButton';

/*
Internal Dependencies
*/
import * as authActions from 'ClientApp/actions/authActions';

class ForgotPasswordPage extends React.Component {
    constructor(props, context) {
        super(props, context);

        this.state = {
            username:''
        }
    }

    submitForm = e => {
        e.preventDefault();
        this.props.actions.forgotPassword(this.state.username);
    }

    handleChange = e => {
        const { name, value } = e.currentTarget;
        this.setState({ [name]: value });
    }

    hasUsername() {
        if (this.state.username === '') {
            return false;
        }
        return true;
    }

    canSubmitForm = () => {
        if (this.props.submitting) {
            return false;
        }
        return this.hasUsername();
    }

    render() {
        const { submitting } = this.props;
        return (
            <div style={{textAlign:'center'}} >
                <h1>Forgot Password</h1>
                <form>
                    <TextField
                        name="username"
                        floatingLabelText="Email"
                        disabled={submitting}
                        value={this.state.username}
                        onChange={this.handleChange}
                        errorText={this.props.errors.email}
                    /><br/>
                    <RaisedButton
                        type="submit"
                        disabled={!this.canSubmitForm()}
                        primary
                        label={submitting ? 'Submitting...' : 'Submit'}
                        onClick={this.submitForm}
                    />
                </form>
            </div>
        );
    }
}

ForgotPasswordPage.propTypes = {
    actions: PropTypes.object.isRequired,
}

function mapStateToProps(state, ownProps) {
    return {
        submitting: state.auth.submitting,
        errors: state.auth.errors
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(authActions, dispatch),
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(ForgotPasswordPage);