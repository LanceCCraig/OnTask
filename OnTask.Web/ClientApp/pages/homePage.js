/**
 * External dependencies
 */
import * as React from 'react';

/**
 * Internal dependencies
 */
import logo from 'ClientApp/static/logo_banner.png';
import TaskDialog from './taskDialog';

class HomePage extends React.Component {
    render() {
        return <div className='main-page'>
            <img src={logo} width="250" height="100" alt="OnTask" className='home-logo' />
            <div style={{ height: '1000px', margin: '5px' }} >
                <TaskDialog />
            </div>
        </div>;
    }
}

export default HomePage;
