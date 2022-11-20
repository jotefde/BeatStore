import React from 'react';
import { Col, Container, Row } from 'react-bootstrap';
//import { useMenu } from 'contexts/MenuContext';
import { NavLink, PhoneLink, MailLink } from '../components/atoms';

const MainFooter = () => {
    return (
        <footer id={'MainFooter'}>
            <div id={'FooterContent'}>
                <Container>
                    <Row className={'justify-content-center'}>
                        <Col as={'article'} className={'footerInfo'} xs={12} md={6}>
                            <h3>{'Info'}</h3>
                            <span>Powered by <a href='https://frylan.online' target='new'>FryLan</a></span>
                            <span>Copyright {new Date().getFullYear()} prod. Olzoo</span>
                        </Col>
                        <Col as={'article'} className={'footerContact'} xs={12} md={6}>
                            <h3 className={'footerContact__title'}>Contact</h3>
                            <PhoneLink className={'footerContact__tel'} number={'+48 600 35 28 35'} />
                            <MailLink className={'footerContact__mail'} mail={'support@prodolzoo.store'} />
                        </Col>
                    </Row>
                </Container>
            </div>
        </footer>
    );
};

export default MainFooter;