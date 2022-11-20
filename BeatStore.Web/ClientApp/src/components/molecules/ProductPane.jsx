import React from 'react';
import cx from 'classnames';
import { NavLink, PublicImage } from 'components/atoms';

const ProductPane = ({ name, quantity, product_slug, category_slug, className,  price, ...props }) => {
  const productLink = `/produkty/${category_slug}/${product_slug}`;
  return <section className={ cx('productPane', className) } {...props}>
    <NavLink to={productLink} className={'productPane__image'} >
      <PublicImage relSrc={ `products/thumbs/${category_slug}/${product_slug}.jpg` } alt={ name }/>
    </NavLink>
    <h3 className={'productPane__name'}>
      <NavLink to={ productLink }>{ name }</NavLink>
    </h3>
    <span className={'productPane__quantity'}>{ `Opakowanie: ${quantity}` }</span>
    { price && <span className={'productPane__price'}>{ `Cena detaliczna: ${price}` }</span> }
  </section>;
};

export default ProductPane;