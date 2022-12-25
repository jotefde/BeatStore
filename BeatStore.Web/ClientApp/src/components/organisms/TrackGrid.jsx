import React, { useEffect, useState } from 'react';
import cx from 'classnames';
import { Container, Row, Col, Button } from 'react-bootstrap';
import { DescribedPicturePane } from 'components/molecules';
import { connect } from 'react-redux';
import { FaShoppingCart, FaCashRegister } from 'react-icons/fa';
import { useShoppingCart } from 'context/ShoppingCartContext';
import {useLocation, useNavigate} from 'react-router-dom';
import { getAllStock } from 'actions';

const TrackGrid = ({ getStock, getAllStockResponse, className, ...props }) => {
    const shoppingCart = useShoppingCart();
    const [isAddingItem, lockAdding] = useState(false);
    const [tracks, setTracks] = useState([]);

    const addToCart = (id, name, slug, price, description) => {
        if (isAddingItem)
            return;
        lockAdding(true);
        shoppingCart.addItem(id, name, slug, price, description);
        setTimeout(() => lockAdding(false), 200);
    };
    const navigate = useNavigate();
    const location = useLocation();
    useEffect(() => {
        getStock()
    }, [location.pathname])

    useEffect(() => {
        if (!getAllStockResponse.errors && getAllStockResponse.items) {
            const newTracks = getAllStockResponse.items.map(stock => stock.Track);
            setTracks(newTracks);
        }
    }, [getAllStockResponse.items])

    /*tracks = [
        { id: 1, name: 'Track #1', slug: 'cover_test_white-1', price: 25.00, description: 'Common description right here my man...' },
        { id: 2, name: 'Freaky track dude', slug: 'cover_test_white-1', price: 25.00, description: 'Common description right here my man...' },
        { id: 3, name: 'Track #2', slug: 'cover_test_white-1', price: 25.00, description: 'Common description right here my man...' },
        { id: 4, name: 'Premium track', slug: 'cover_test_premium-3', price: 150.00, description: 'Common description right here my man...' },
        { id: 5, name: 'Lighten track', slug: 'cover_test_premium-4', price: 80.00, description: 'Common description right here my man...' },
        { id: 6, name: 'Another track', slug: 'cover_test_white-1', price: 25.00, description: 'Common description right here my man...' },
        { id: 7, name: 'Track #3', slug: 'cover_test_white-1', price: 25.00, description: 'Common description right here my man...' },
        { id: 8, name: 'Yeah boy', slug: 'cover_test_premium-4', price: 80.00, description: 'Common description right here my man...' },
        { id: 9, name: 'Whats is it?', slug: 'cover_test_white-1', price: 25.00, description: 'Common description right here my man...' }
    ];*/
    const mappedTracks = [];
    for (const { Id, Name, Slug, Price, Description } of tracks) {
        const track = <Col className={'trackCard'} xs={12} md={6} lg={4} xl={3} key={Id}>
            <DescribedPicturePane className={'trackCard__content'} picture={`http://localhost:9000/covers/${Slug}.jpg`} link={`/product/${Slug}`} title={Name} price={Price}/>
            <Container className={'buttonWrap'}>
                <Row>
                    <Col as={Button} variant='outline-primary' xs={6}
                        className={cx('button--outlined', 'button')}
                        disabled={isAddingItem}
                        onClick={e => {
                            e.preventDefault();
                            addToCart(Id, Name, Slug, Price, Description);
                        } }>
                        <span>{'Add to cart '}</span> <FaShoppingCart />
                    </Col>
                    <Col as={Button}
                         variant="success"
                         className={cx('button')}
                         onClick={e => {
                             e.preventDefault();
                             addToCart(Id, Name, Slug, Price, Description);
                             navigate('/shopping-cart');
                         } }>
                        <span>{'Buy now! '}</span> <FaCashRegister />
                    </Col>
                </Row>
            </Container>
        </Col>;
        mappedTracks.push(track);
    }

    return <Row>{mappedTracks}</Row>;
};

const mapDispatchToProps = dispatch => ({
    getStock: () => dispatch(getAllStock)
})

const mapStateToProps = ({ getAllStockResponse }) => ({ getAllStockResponse });

export default connect(mapStateToProps, mapDispatchToProps)(TrackGrid);