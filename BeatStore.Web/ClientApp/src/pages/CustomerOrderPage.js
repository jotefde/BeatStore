import React, {useEffect, useState} from 'react';
import {useParams} from 'react-router-dom';
import {connect} from 'react-redux';
import {Container, Row, Col} from 'react-bootstrap';
import { useGetCustomerOrder } from 'actions';
import cx from 'classnames';
import ClipLoader from "react-spinners/ClipLoader";
import useDataLoading from 'hooks/useDataLoading';

const CustomerOrderPage = ({...props}) => {
    const { accessKey } = useParams();
    const { isLoading, error, data } = useGetCustomerOrder(accessKey);

    const dataLoading = useDataLoading(isLoading, error);
    if(dataLoading)
        return dataLoading;

    //const tracks = order.Items.map(item => item.Track);
    return (
        <Container as={'section'} id={'CustomerOrder'} className={'pageContent'}>
            <h1 className={'pageTitle'}>{'Order'}</h1>
            <Row className={cx('justify-content-center', '')}>

            </Row>
        </Container>
    );
}

export default CustomerOrderPage;