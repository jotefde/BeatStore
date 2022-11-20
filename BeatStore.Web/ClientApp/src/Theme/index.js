import React from 'react';
import MainHeader from './Header';
import MainFooter from './Footer';
import { IoArrowUpCircleOutline } from 'react-icons/io5';
import { Container, Row } from 'react-bootstrap';

import 'styles/App.scss';

const Theme = ({ children }) => {
    return <>
        <MainHeader />
        {children}
        <MainFooter />
        <IoArrowUpCircleOutline id={'ScrollUpButton'} onClick={() => window.scrollTo({ top: 0, behavior: 'smooth' })} />
    </>;
};

export default Theme;
