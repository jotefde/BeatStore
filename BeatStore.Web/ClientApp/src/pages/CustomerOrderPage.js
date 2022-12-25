import React, {Component, useEffect, useState} from 'react';
import {useNavigate, useParams} from 'react-router-dom';
import {connect} from 'react-redux';
import {Container, Row, Col, Button} from 'react-bootstrap';
import {getStock} from 'actions';
import {CoverImage} from 'components/atoms';
import {BsCheckCircle} from 'react-icons/bs';
import cx from 'classnames';
import {FaCashRegister, FaShoppingCart} from "react-icons/fa";
import {useShoppingCart} from "context/ShoppingCartContext";

export const CustomerOrderPage = ({getSingleOrder, orderResponse, ...props}) => {
    const {slug} = useParams();
    const {Stock, Trackout} = stockResponse;
    const shoppingCart = useShoppingCart();
    const [isAddingItem, lockAdding] = useState(false);
    const navigate = useNavigate();

    useEffect(() => {
        getSingleStock(slug);
    }, [slug])

    if (Stock == null)
        return <></>;
    const {Track} = Stock;

    const addToCart = ({Id, Name, Slug, Price, Description}) => {
        if (isAddingItem)
            return;
        lockAdding(true);
        shoppingCart.addItem(Id, Name, Slug, Price, Description);
        setTimeout(() => lockAdding(false), 200);
    };

    return (
        <Container as={'section'} id={'CustomerOrder'} className={'pageContent'}>
            <Row className={cx('justify-content-center', '')}>
            </Row>
        </Container>
    );
}

const mapDispatchToProps = dispatch => ({
    getSingleStock: (slug) => dispatch(getStock(slug))
})

const mapStateToProps = ({getStockResponse}) => ({stockResponse: getStockResponse});

export default connect(mapStateToProps, mapDispatchToProps)(TrackPage);