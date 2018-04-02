/**
 * External dependencies
 */
import React from 'react';
import PropTypes from 'prop-types';
import RaisedButton from 'material-ui/RaisedButton';
import {
    Toolbar,
    ToolbarGroup
} from 'material-ui/Toolbar';

const RecommendationsToolbar = ({ handleClick }) => {
    return (
        <div>
            <Toolbar>
                <ToolbarGroup style={{ float: 'none', margin: 'auto' }}>
                    <RaisedButton
                        primary={true}
                        label="Recalculate"
                        onClick={handleClick}
                    />
                </ToolbarGroup>
            </Toolbar>
        </div>
    );
};

RecommendationsToolbar.propTypes = {
    handleClick: PropTypes.func.isRequired
};

export default RecommendationsToolbar;
