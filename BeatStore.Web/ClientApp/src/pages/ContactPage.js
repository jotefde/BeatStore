import React, { Component } from 'react';
import { Col, Container, Row } from 'react-bootstrap';
import { ContactForm } from 'components/organisms';
import { MailLink, PhoneLink } from 'components/atoms';

export class ContactPage extends Component {
    static displayName = ContactPage.name;

    render() {
        return (
            <Container as={'article'} id={'Contact'}>
                <h1>Contact</h1>
                <Row className={'justify-content-center'}>
                    <Col as={'article'} sm={12} lg={6} className={'contactDetails'}>
                        <section className={'contactDetails__section'}>
                            <h3>{'Collaboration'}</h3>
                            <div className='content'>
                                <p>{'Would you like to cooperate with us?'}</p>
                                <p>{'Go ahead, we will consider every proposal 😉'}</p>
                                <PhoneLink number={'+48 600 35 28 35'} />
                                <MailLink mail={'collab@prodolzoo.store'} />
                            </div>
                        </section>

                        <section className={'contactDetails__section'}>
                            <h3>{'Customer service'}</h3>
                            <div className='content'>
                                <p>{'In case of any questions or problems with the order, we are available at this e-mail address:'}</p>
                                <MailLink mail={'orders@prodolzoo.store'} />
                            </div>
                        </section>

                        <section className={'contactDetails__section'}>
                            <h3>{'Technical support'}</h3>
                            <div className='content'>
                                <p>{'Found some bug?'}</p>
                                <p>{'Feel free to report it.'}</p>
                                <MailLink mail={'support@prodolzoo.store'} />
                            </div>
                        </section>
                    </Col>
                    <Col as={ContactForm} sm={12} lg={6} />
                </Row>
            </Container>
        );
    }
}
