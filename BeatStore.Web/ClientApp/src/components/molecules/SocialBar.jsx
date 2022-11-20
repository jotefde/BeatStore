import React from 'react';
import { PhoneLink, PublicImage } from '../components/atoms';
import { Col, Container, Row } from 'react-bootstrap';
import cx from 'classnames';

const SocialBar = (props) => {
    return (
        <header id={'SocialBar'} {...props}>
            <Container className={'content'}>
                <Row className={cx('justify-content-center', 'topBar')}>
                    <Col as={navLink} to={"/"} xs={4} className={'topBar__logo'}>
                        <h1>{'prod olzoo'}</h1>
                    </Col>
                    <Col xs={8} className={'topBar__wrap'}>
                        <MenuBar />
                    </Col>
                </Row>
                {/*<Row className={cx('justify-content-center', 'searchBar')}>
                    <Col xs={12}>
                        <SearchForm />
                    </Col>
                </Row>*/}
                <Row className={cx('justify-content-center', 'socialBar')}>
                    <Col xs={12} md={4}>
                    </Col>
                </Row>
            </Container>
        </header>
    );
};

export default MainHeader;