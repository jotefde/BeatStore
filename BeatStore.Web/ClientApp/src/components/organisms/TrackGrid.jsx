import React, { useEffect, useState } from 'react';
import cx from 'classnames';
import { Container, Row, Col, Button } from 'react-bootstrap';
import { DescribedPicturePane } from 'components/molecules';
import { FaShoppingCart, FaCashRegister } from 'react-icons/fa';
import { useShoppingCart } from 'context/ShoppingCartContext';
import {useNavigate} from 'react-router-dom';
import { useListStock } from 'actions';
import useDataLoading from "hooks/useDataLoading";
import ClipLoader from "react-spinners/ClipLoader";

const TrackGrid = ({ className, ...props }) => {
    const shoppingCart = useShoppingCart();
    const [isAddingItem, lockAdding] = useState(false);

    const addToCart = (id, name, slug, price, description) => {
        if (isAddingItem)
            return;
        lockAdding(true);
        shoppingCart.addItem(id, name, slug, price, description);
        setTimeout(() => lockAdding(false), 200);
    };
    const navigate = useNavigate();

    const { isLoading, isError, isSuccess, error, data } = useListStock();

    const dataLoading = useDataLoading(isLoading, error);
    if(dataLoading)
        return dataLoading;

    const tracks = data?.map(stock => stock.Track) ?? [];
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

export default TrackGrid;