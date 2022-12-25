import React from 'react';
import { NavLink, PublicImage } from 'components/atoms';
import cx from 'classnames';

const DescribedPicturePane = ({ picture, link, title, price, className, children, ...props }) => {
    let PictureElement = <PublicImage relSrc={picture} alt={title} />;
    if (picture.startsWith('http') || picture.startsWith('https'))
        PictureElement = <img src={picture} alt={title} />;
    return (
        <section className={cx('describedPicturePane', className)} {...props}>
            <NavLink to={link} className={'describedPicturePane__image'}>
                {PictureElement}

                {price ? <span className={'describedPicturePane__price'}>{price.toLocaleString('pl-PL', { style: 'currency', currency: 'PLN' })}</span> : <></>}
            </NavLink>
            <header className={'describedPicturePane__title'}>
                <NavLink to={link}>{title}</NavLink>
            </header>
            <div className={'describedPicturePane__content'}>
                {children ?? <></>}
            </div>
        </section>
    );
};

export default DescribedPicturePane;