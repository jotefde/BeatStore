import React, { useRef } from 'react';
import { NavLink } from 'components/atoms';
import MenuBar from './MenuBar';
import { Col, Container, Row } from 'react-bootstrap';
import cx from 'classnames';
import { FaInstagram, FaTwitter, FaYoutube, FaShoppingCart } from 'react-icons/fa';
import { useShoppingCart } from 'context/ShoppingCartContext';
import { ShoppingCartPreview } from 'components/organisms';

const MainHeader = (props) => {
    const shoppingCart = useShoppingCart();
    const shoppingCartPreviewRef = useRef(null);
    const toggleSCPreview = e => {
        if (!window.matchMedia('(min-width: 768px)').matches)
            return;
        e.preventDefault();
        if (shoppingCartPreviewRef.current)
            shoppingCartPreviewRef.current.classList.toggle('shoppingCartPreview--visible');
    };

    return (
        <header id={'MainHeader'} {...props}>

            <NavLink to={'/'} id={'MainLogo'}>
                <h1>{'prod olzoo'}</h1>
            </NavLink>

            <ShoppingCartPreview selfRef={shoppingCartPreviewRef} className={cx('shoppingCartPreview') } />

            <NavLink to={'/shopping-cart'} className={'shoppingCartButton'} onClick={toggleSCPreview}>
                <FaShoppingCart className={'shoppingCartButton__icon'} />
                <span className={'shoppingCartButton__quantity'}>{shoppingCart.getQuantity()}</span>
            </NavLink>

            <Container className={'content'}>
                <Row className={cx( 'content__menu') }>
                    <Col xs={12}>
                        <MenuBar />
                    </Col>
                </Row>
                {/*<Row className={cx('justify-content-center', 'searchBar')}>
                    <Col xs={12}>
                        <SearchForm />
                    </Col>
                </Row>*/}
                <Row className={cx('justify-content-center', 'socialBar')}>
                    <Col as={'article'} className={'socialButton'} xs={3} md={2}>
                        <a className={'socialButton__content'} href={'https://www.facebook.com/JoteFDe99'}>
                            <FaTwitter className={'icon'} />
                            <strong className={'name'}>{ '@olzoo' }</strong>
                        </a>
                    </Col>
                    <Col as={'article'} className={'socialButton'} xs={3} md={2}>
                        <a className={'socialButton__content'} href={'https://www.youtube.com/user/rtrans1979'}>
                            <FaYoutube className={'icon'} />
                            <strong className={'name'}>{ 'prod olzoo' }</strong>
                        </a>
                    </Col>
                    <Col as={'article'} className={'socialButton'} xs={3} md={2}>
                        <a className={'socialButton__content'} href={'https://www.instagram.com/olzooo/'}>
                            <FaInstagram className={'icon'} />
                            <strong className={'name'}>{ '@olzooo' }</strong>
                        </a>
                    </Col>
                </Row>
            </Container>
        </header>
    );
};

export default MainHeader;