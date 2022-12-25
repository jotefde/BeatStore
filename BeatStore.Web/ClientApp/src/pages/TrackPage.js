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

export const TrackPage = ({getSingleStock, stockResponse, ...props}) => {
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
        <Container as={'section'} id={'TrackInfo'} className={'pageContent'}>
            <Row className={cx('justify-content-center', 'trackMainContent')}>
                <Col xs={12} lg={6} className={'trackMainContent__imageWrap'}>
                    <CoverImage slug={Track.Slug} alt={Track.Name}/>
                </Col>
                <Col xs={12} lg={6}>
                    <h3 className={'trackMainContent__name'}>{Track.Name}</h3>
                    <div className={'trackMainContent__price'}>
                        <span className={'spanTitle'}>Price</span>
                        <span>{Track.Price.toLocaleString('pl-PL', {
                            style: 'currency',
                            currency: 'PLN'
                        })}</span>
                    </div>
                    <div className={'trackMainContent__status'}>
                        <BsCheckCircle/>
                        <span>{'In stock'}</span>
                    </div>
                    <div className={'trackMainContent__description'}>
                        <h5 className={'sectionTitle'}>Description</h5>
                        <p>{Track.Description}</p>
                    </div>
                    <Container className={'trackMainContent__buttons'}>
                        <Row>
                            <Col as={Button} variant='outline-primary' xs={6}
                                 className={cx('button--outlined', 'button')}
                                 disabled={isAddingItem}
                                 onClick={e => {
                                     e.preventDefault();
                                     addToCart(Track);
                                 } }>
                                <span>{'Add to cart '}</span> <FaShoppingCart />
                            </Col>
                            <Col as={Button}
                                 variant="success"
                                 className={cx('button')}
                                 onClick={e => {
                                     e.preventDefault();
                                     addToCart(Track);
                                     navigate('/shopping-cart')
                                 } }>
                                <span>{'Buy now! '}</span> <FaCashRegister />
                            </Col>
                        </Row>
                    </Container>
                </Col>
            </Row>
            <Row className={cx('justify-content-center', 'trackDetails')}>
                <Col className={'trackDetails__sample'} xs={12} lg={6}>
                    <h5>Sample playback</h5>
                    <figure className={'playback'}>
                        <audio
                            controls
                            src={ `http://localhost:5225/track-storage/${Track.Id}/sample` }>
                            <a href={ `http://localhost:5225/track-storage/${Track.Id}/sample` }>
                                Download sample
                            </a>
                        </audio>
                    </figure>

                </Col>
                <Col className={'trackDetails__trackout'} xs={12} lg={6}>
                    <h5>Trackout</h5>
                    <ol>
                        {Trackout.map(t => <li className={'trackoutItem'} key={t}>{t.replace('.wav', '')}</li>)}
                    </ol>
                </Col>
            </Row>
        </Container>
    );
}

const mapDispatchToProps = dispatch => ({
    getSingleStock: (slug) => dispatch(getStock(slug))
})

const mapStateToProps = ({getStockResponse}) => ({stockResponse: getStockResponse});

export default connect(mapStateToProps, mapDispatchToProps)(TrackPage);