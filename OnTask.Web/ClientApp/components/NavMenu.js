/**
 * External dependencies
 */
import * as React from 'react';
import { NavLink, Link } from 'react-router-dom';
import logo from './logo_banner.png'


export class NavMenu extends React.Component {
    render() {
        return <div className='main-nav'>
                <div className='navbar navbar-inverse'>
                <div className='navbar-header'>
                    <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                    </button>
                    <Link className='navbar-brand' to={'/'}> <img src={logo} width="100" height="40" alt="OnTask"/> </Link>
                </div>
                <div className='clearfix'></div>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>
                        <li>
                            <NavLink exact to={ '/' } activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Home
                            </NavLink>
                        </li>
                        <li>
                            <NavLink exact to={ '/login' } activeClassName='active'>
                                <span className='glyphicon glyphicon-log-in'></span> Login
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={"/counter"} activeClassName="active">
                                <span className="glyphicon glyphicon-education" /> Counter
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={"/fetchdata"} activeClassName="active">
                                <span className="glyphicon glyphicon-th-list" /> Fetch data
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={"/calendar"} activeClassName="active">
                                <span className="glyphicon glyphicon-calendar" /> Calendar
                            </NavLink>
                        </li>
                        {/* <li>
                            <NavLink to={"/task"} activeClassName="active">
                                <span className="glyphicon glyphicon-check" /> Tasks
                            </NavLink>
                        </li> */}
                    </ul>
                </div>
            </div>
        </div>;
    }
}
