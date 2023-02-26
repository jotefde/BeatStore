import React from 'react';
import {useParams} from 'react-router-dom';
import {Container, Row, Col} from 'react-bootstrap';
import { useGetCustomerOrder } from 'actions';
import cx from 'classnames';
import useDataLoading from 'hooks/useDataLoading';
import {CoverImage} from "components/atoms";
import { API_URL} from "Constants";

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
                    <div className={'paymentInfo__warning'}>
                        <p>Warning!</p>
                        <p>If it is not your order, you have to leave this page right now.</p>
                    </div>
                    <h3 className={'paymentInfo__paymentId'}>Order <span>{Order.PaymentId}</span></h3>
                    <table>
                        <tbody>
                            <tr>
                                <td>Name: </td> <td>{Order.CustomerFirstName} {Order.CustomerLastName}</td>
                            </tr>
                            <tr>
                                <td>Email: </td> <td>{Order.CustomerEmail}</td>
                            </tr>
                            <tr>
                                <td>Status: </td> <td>{Order.Status}</td>
                            </tr>
                            <tr>
                                <td>Description: </td> <td>{Order.Description}</td>
                            </tr>
                        </tbody>
                    </table>
                </Col>
            </Row>
            <Row className={cx('justify-content-center', '')}>
                <Col xs={12} className={'orderItems'}>
                    <h3 className={'orderItems__title'}>Your items</h3>
                    <Container>
                        {tracks.map(
                            ({Id, Name, Slug}) =>  {
                                const objectUrl = obj => `${API_URL}/track-storage/${Id}/${obj}?accessKey=${accessKey}`;
                                return (
                                    <Row className={'track'} key={Id}>
                                        <Col xs={12} lg={4} className={'track__cover'}>
                                            <CoverImage slug={Slug} alt={Slug} />
                                        </Col>
                                        <Col xs={12} lg={8} className={'track__content'}>
                                            <b className={'track__name'}>{Name}</b>
                                            <table className={'track__files'}>
                                                <thead>
                                                    <tr>
                                                        <th>File name</th> <th>Size</th> <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                {
                                                    TrackObjects[Id].map(
                                                        object => <tr key={object.Id}>
                                                            <td>{object.Name}</td>
                                                            <td>{(object.Size / (1000 * 1000)).toFixed(2)} MB</td>
                                                            <td><a href={objectUrl(object.ObjectType)} target={'_blank'}>Download</a></td>
                                                        </tr>
                                                    )
                                                }
                                                {
                                                    TrackObjects[Id].length === 0 && <tr>
                                                        <td colSpan={3} className={'emptyTrackout'}>No files included.<br/> Please contact our support.</td>
                                                    </tr>
                                                }
                                                </tbody>
                                            </table>
                                        </Col>
                                    </Row>)
                            })
                        }
                    </Container>
                </Col>
            </Row>
        </Container>
    );
}

export default CustomerOrderPage;