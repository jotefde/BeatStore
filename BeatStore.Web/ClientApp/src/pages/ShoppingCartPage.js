import React, { Component } from 'react';
import { Col, Container, Row } from 'react-bootstrap';
import { MailLink, PhoneLink } from 'components/atoms';

export class ShoppingCartPage extends Component {
    static displayName = ShoppingCartPage.name;

    render() {
        return (
            <Container as={'article'} id={'ShoppingCart'}>
                <h1>Shopping cart</h1>
                <Row className={'justify-content-center'}>

                </Row>
            </Container>
        );
    }
}
