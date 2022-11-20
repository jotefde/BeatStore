import React, { useEffect, useRef, useState } from 'react';
import { FaBars, FaAngleDown, FaAngleUp } from 'react-icons/fa';
import cx from 'classnames';
import { NavLink, PublicImage } from 'components/atoms';
import { getProductCategories } from 'actions';
import { connect } from 'react-redux';

const menu = [
    { name: 'Home', path: '/' },
    { name: 'Contact', path: '/contact' },
    { name: 'About Us', path: '/about-us' },
];

const menuItemElement = ({ name, path }) => <li key={name} className={'menuItem'}>
    <NavLink to={path} className={ 'menuItem__link' }>{name}</NavLink>
</li>;

const MenuBar = ({ categories, getCategories, ...props }) => {

    useEffect(() => {
        //getCategories();
    }, []);

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

const mapDispatchToProps = dispatch => ({
    /*getCategories: (getCategories) => dispatch(getCategories)*/
})

const mapStateToProps = ({ categories }) => ({ categories });

export default connect(mapStateToProps, mapDispatchToProps)(MenuBar);