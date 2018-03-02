/**
 * External dependencies
 */
import React from 'react';

/**
 * Internal dependencies
 */
import { NavMenu } from 'ClientApp/components/navMenu';

export default class Layout extends React.Component {
    render() {
        return <div className='container-fluid'>
            <div className='row'>
                <div className='col-sm-3'>
                    <NavMenu />
                </div>
                <div className='col-sm-9'>
                    { this.props.children }
                </div>
            </div>
        </div>;
    }
}
