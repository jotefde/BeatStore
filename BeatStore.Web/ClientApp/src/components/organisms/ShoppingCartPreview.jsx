import React, { useState, useEffect } from 'react';
import { useShoppingCart } from 'context/ShoppingCartContext';
import { NavLink } from 'components/atoms';
import { FaRegMinusSquare, FaShoppingCart, FaCashRegister } from 'react-icons/fa';
import { Button } from 'react-bootstrap';
import cx from 'classnames';

const ShoppingCartPreview = ({ selfRef, ...props }) => {
    const shoppingCart = useShoppingCart();
    const [rows, setRows] = useState([]);
    const removeItem = id => {
        shoppingCart.removeItem(id);
    }

    useEffect(() => {
        if (shoppingCart.getQuantity() === 0)
            return;

        let no = 1;
        const newRows = [];
        for (const { id, name, slug, price } of shoppingCart.items) {
            const cells = [
                <FaRegMinusSquare className='cartPreviewList__removeItemButton' onClick={ () => removeItem(id) }/>,
                `${no}.`,
                <NavLink to={ `/track/${slug}` }>{name}</NavLink>,
                price.toLocaleString('pl-PL', { style: 'currency', currency: 'PLN' })
            ]
                .map(
                    (value, index) => <td key={`value-${index}`}>{value}</td>
                );
            newRows.push(<tr key={id}>{cells}</tr>);
            no++;
        }
        setRows(newRows);
    }, [shoppingCart.items]);

    const content = <>
        <table className='cartPreviewList'>
            <thead>
                <tr>
                    <th> </th>
                    <th>#</th>
                    <th>Name</th>
                    <th>Price</th>
                </tr>
            </thead>
            <tbody>
                {rows}
            </tbody>

            <tfoot className='cartPreviewList__totalPrice'>
                <tr>
                    <td colSpan={3}>Total price:</td>
                    <td>{shoppingCart.getTotalPrice().toLocaleString('pl-PL', { style: 'currency', currency: 'PLN' })}</td>
                </tr>
            </tfoot>
        </table>

        <div className='buttons'>
            <Button as={NavLink} to='/shopping-cart' variant='outline-primary' className={cx('button--outlined', 'button')}>
                <span>Go to cart</span> <FaShoppingCart />
            </Button>
            <Button as={NavLink} to='/order' variant='success' className='button'>
                <span>Buy!</span> <FaCashRegister />
            </Button>
        </div>
        </>;

    return <div id='ShoppingCartPreview' ref={selfRef} {...props}>
        <p className='title'>Tracks in your cart:</p>
        <div className='content'>
            {shoppingCart.getQuantity() ? content : <p>Cart is empty :(</p>}
            
        </div>
    </div>;
};

export default ShoppingCartPreview;