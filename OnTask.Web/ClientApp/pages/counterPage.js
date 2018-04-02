/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';

/**
 * Internal dependencies
 */
import * as counterStore from 'ClientApp/store/Counter';

class CounterPage extends React.Component {
    onClickHandler = (event) => {
        event.preventDefault();
        this.props.increment();
    }
    render() {
        return (
            <div>
                <h1>Counter</h1>
                <p>This is a simple example of a React component.</p>
                <p>Current count: <strong>{this.props.count}</strong></p>
                <button onClick={this.onClickHandler}>Increment</button>
            </div>
        );
    }
}

// Wire up the React component to the Redux store
export default connect(
    (state) => state.counter, // Selects which state properties are merged into the component's props
    counterStore.actionCreators                 // Selects which action creators are merged into the component's props
)(CounterPage);
