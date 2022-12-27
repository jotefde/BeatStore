import React, {useEffect, useState} from 'react';
import {useParams} from 'react-router-dom';
import {connect} from 'react-redux';
import {Container, Row, Col} from 'react-bootstrap';
import { useGetCustomerOrder, API_URL } from 'actions';
import cx from 'classnames';
import useDataLoading from 'hooks/useDataLoading';

const CustomerOrderPage = ({...props}) => {
    const { accessKey } = useParams();
    const { isLoading, error, data } = useGetCustomerOrder(accessKey);

    const dataLoading = useDataLoading(isLoading, error);
    if(dataLoading)
        return dataLoading;

    const {Order, TrackObjects} = data;
    const tracks = Order.Items.map(item => item.Track);
    return (
        <Container as={'section'} id={'CustomerOrder'} className={'pageContent'}>
            <Row className={cx('justify-content-center', '')}>
                <Col xs={12} className={'paymentInfo'}>
                    <p className={'paymentInfo__paymentId'}>Order number: <span>{Order.PaymentId}</span></p>
                    <p className={'paymentInfo__customerName'}>Name: <span>{Order.CustomerFirstName} {Order.CustomerLastName}</span></p>
                    <p className={'paymentInfo__customerEmail'}>Email: <span>{Order.CustomerEmail}</span></p>
                    <p className={'paymentInfo__description'}>Email: <span>{Order.Description}</span></p>
                    <p className={'paymentInfo__status'}>Email: <span>{Order.Status}</span></p>
                </Col>
            </Row>
            <Row className={cx('justify-content-center', '')}>
                <Col xs={12} className={'orderItems'}>
                    <p className={'orderItems__title'}></p>
                    {tracks.map(
                        ({Id, Name, Slug}) =>  {
                            const {WaveFile, SampleFile, TrackoutFile} = TrackObjects[Id];
                            const objectUrl = obj => `${API_URL}/track-storage/${Id}/${obj}?accessKey=${accessKey}`;
                            return (
                                <div className={'track'}>
                                    <div className={'track__cover'}>
                                        <img src={`http://localhost:9000/covers/${Slug}.jpg`} alt={Slug}/>
                                    </div>
                                    <p className={'track__name'}>{Name}</p>
                                    <ul className={'track__files'}>
                                        <li>
                                            <a href={objectUrl('wave')} download>{WaveFile}</a>
                                        </li>
                                        <li>
                                            <a href={objectUrl('trackout')} download>{TrackoutFile}</a>
                                        </li>
                                        <li>
                                            <a href={objectUrl('sample')} download>{SampleFile}</a>
                                        </li>
                                    </ul>
                                </div>)
                        })
                    }
                </Col>
            </Row>
        </Container>
    );
}

export default CustomerOrderPage;