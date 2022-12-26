import React, {Component, useEffect, useState} from 'react';
import {useLocation, useNavigate, useParams} from 'react-router-dom';
import {connect} from 'react-redux';
import {Container, Row, Col, Button} from 'react-bootstrap';
import { getCustomerOrder } from 'actions';
import {CoverImage} from 'components/atoms';
import cx from 'classnames';

const CustomerOrderPage = ({getOrder, orderResponse, ...props}) => {
    const { accessKey } = useParams();
    const location = useLocation();
    const [order, setOrder] = useState(null);
    const [trackObjects, setTrackObjects] = useState(null);
    const [isMounted, mount] = useState(false);
    const [isLoaded, setLoad] = useState(false);

    useEffect(() => {
        mount(true);
    }, [])

    useEffect(() => {
        if(isMounted)
            getOrder(accessKey);
    }, [accessKey, isMounted]);

    useEffect(() => {
        if(orderResponse.length <= 0)
            return;
        setLoad(true);
        const {Order, TrackObjects} = orderResponse;
        setOrder(Order);
        setTrackObjects(TrackObjects);
    }, [orderResponse]);

    if(!isLoaded)
        return <></>;
    console.log(order);

    //const tracks = order.Items.map(item => item.Track);
    return (
        <Container as={'section'} id={'CustomerOrder'} className={'pageContent'}>
            <h1 className={'pageTitle'}>{'Order'}</h1>
            <Row className={cx('justify-content-center', '')}>

            </Row>
        </Container>
    );
}

const mapDispatchToProps = dispatch => ({
    getOrder: (accessKey) => dispatch(getCustomerOrder(accessKey))
})

const mapStateToProps = ({getCustomerOrderResponse}) => ({orderResponse: getCustomerOrderResponse});

export default connect(mapStateToProps, mapDispatchToProps)(CustomerOrderPage);