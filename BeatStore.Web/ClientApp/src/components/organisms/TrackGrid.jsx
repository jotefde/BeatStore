import React, { useState } from 'react';
import cx from 'classnames';
import { Container, Row, Col, Button } from 'react-bootstrap';
import { DescribedPicturePane } from 'components/molecules';
import { connect } from 'react-redux';
import { FaShoppingCart, FaCashRegister } from 'react-icons/fa';
import { useShoppingCart } from 'context/ShoppingCartContext';

const TrackGrid = ({ className, tracks, ...props }) => {
    const shoppingCart = useShoppingCart();
    const [isAddingItem, lockAdding] = useState(false);

    const addToCart = (id, name, slug, price, description) => {
        if (isAddingItem)
            return;
        lockAdding(true);
        shoppingCart.addItem(id, name, slug, price, description);
        setTimeout(() => lockAdding(false), 200);
    };

    tracks = [
        { id: 1, name: 'Track #1', slug: 'cover_test_white-1', price: 25.00, description: 'Common description right here my man...' },
        { id: 2, name: 'Freaky track dude', slug: 'cover_test_white-1', price: 25.00, description: 'Common description right here my man...' },
        { id: 3, name: 'Track #2', slug: 'cover_test_white-1', price: 25.00, description: 'Common description right here my man...' },
        { id: 4, name: 'Premium track', slug: 'cover_test_premium-3', price: 150.00, description: 'Common description right here my man...' },
        { id: 5, name: 'Lighten track', slug: 'cover_test_premium-4', price: 80.00, description: 'Common description right here my man...' },
        { id: 6, name: 'Another track', slug: 'cover_test_white-1', price: 25.00, description: 'Common description right here my man...' },
        { id: 7, name: 'Track #3', slug: 'cover_test_white-1', price: 25.00, description: 'Common description right here my man...' },
        { id: 8, name: 'Yeah boy', slug: 'cover_test_premium-4', price: 80.00, description: 'Common description right here my man...' },
        { id: 9, name: 'Whats is it?', slug: 'cover_test_white-1', price: 25.00, description: 'Common description right here my man...' }
    ];
    const mappedTracks = [];
    for (const { id, name, slug, price, description } of tracks) {
        const track = <Col className={'trackCard'} xs={12} md={6} lg={4} xl={3} key={id}>
            <DescribedPicturePane className={'trackCard__content'} picture={`covers/${slug}.jpg`} link={`/product/${slug}`} title={name} price={price}/>
            <Container className={'buttonWrap'}>
                <Row>
                    <Col as={Button} variant='outline-primary' xs={6}
                        className={cx('button--outlined', 'button')}
                        disabled={isAddingItem}
                        onClick={e => {
                            e.preventDefault();
                            addToCart(id, name, slug, price, description);
                        } }>
                        <span>{'Add to cart '}</span> <FaShoppingCart />
                    </Col>
                    <Col as={Button} variant="success" className={cx('button')}>
                        <span>{'Buy now! '}</span> <FaCashRegister />
                    </Col>
                </Row>
            </Container>
        </Col>;
        mappedTracks.push(track);
    }

    return <Row>{mappedTracks}</Row>;
};

const mapStateToProps = ({ tracks }) => ({ tracks });

export default connect(mapStateToProps, dispatch => ({}))(TrackGrid);