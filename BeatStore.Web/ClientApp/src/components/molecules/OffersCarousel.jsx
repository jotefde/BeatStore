import React from 'react';
import { Carousel } from 'react-responsive-carousel';
import "react-responsive-carousel/lib/styles/carousel.min.css";
import { ToOfferButton } from 'components/atoms';

const OffersCarousel = (props) => {
  return (
    <Carousel autoPlay={true} infiniteLoop={true} showStatus={false} showThumbs={false} { ...props }>
      <div className={ 'carouselItem' }>
        <h2 className={ 'carouselItem__title' }>Produkty ekologiczne</h2>
        <p className={ 'carouselItem__text' }>Odżywiaj się zdrowo z Pro Natura!</p>
        <ToOfferButton />
      </div>
      <div className={ 'carouselItem' }>
        <h2 className={ 'carouselItem__title' }>Zdrowa żywność</h2>
        <p className={ 'carouselItem__text' }>Postaw na wyśmienity smak, który zapewnia żywność naturalna.</p>
        <ToOfferButton />
      </div>
    </Carousel>
  );
};

export default OffersCarousel;