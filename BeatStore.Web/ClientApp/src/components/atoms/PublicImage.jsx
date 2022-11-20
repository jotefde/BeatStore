import React from 'react';

const PublicImage = ({ alt, relSrc, ...props }) => {
  return <img src={`${process.env.PUBLIC_URL}/images/${relSrc}`} alt={alt} {...props}/>;
}


export default PublicImage;