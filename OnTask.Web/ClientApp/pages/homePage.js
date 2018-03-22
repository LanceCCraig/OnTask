/**
 * External dependencies
 */
import * as React from 'react';
import RaisedButton from 'material-ui/RaisedButton';

/**
 * Internal dependencies
 */
import authHelper from 'ClientApp/helpers/authHelper';
import logo from 'ClientApp/components/logo_banner.png';
import TaskDialog from './taskDialog';

class HomePage extends React.Component {
    render() {
        return <div className='main-page'>
            <img src={logo} width="250" height="100" alt="OnTask" className='home-logo' />
            <div className='center' style={{ height: '1000px', margin: '5px' }} >
                <TaskDialog />
            </div>
        </div>;
    }
}

export default HomePage;
