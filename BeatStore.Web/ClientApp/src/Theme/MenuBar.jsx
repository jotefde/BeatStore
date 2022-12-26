import React, { useEffect, useRef } from 'react';
import { FaBars } from 'react-icons/fa';
import cx from 'classnames';
import { NavLink } from 'components/atoms';

const menu = [
    { name: 'Home', path: '/' },
    { name: 'Contact', path: '/contact' },
    { name: 'About Us', path: '/about-us' },
];

const menuItemElement = ({ name, path }) => <li key={name} className={'menuItem'}>
    <NavLink to={path} className={ 'menuItem__link' }>{name}</NavLink>
</li>;

const MenuBar = ({ ...props }) => {

    const mainMenuWrapRef = useRef(null);

    const handleHamburgerClick = e => {
        mainMenuWrapRef.current.classList.toggle('mainMenuWrap--active');
    };

    const menuItems = menu.map(item => menuItemElement(item));

    return (
        <nav id={'MenuBar'} {...props}>
            <FaBars fill={'rgb(255, 255, 255)'} color={'rgb(255, 255, 255)'} title={'Menu'} className={'hamburgerButton'} onClick={handleHamburgerClick} />

            <section className={'mainMenuWrap'} ref={mainMenuWrapRef}>
                <div className={'mainMenuWrap__closeButton'} onClick={handleHamburgerClick} >
                    {'×'}
                </div>

                <ol className={'mainMenu'}>
                    {menuItems}
                </ol>
            </section>
        </nav>
    );
};

export default MenuBar;