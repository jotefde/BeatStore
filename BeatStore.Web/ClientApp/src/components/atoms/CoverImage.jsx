import React from 'react';

const CoverImage = ({ alt, slug, ...props }) => {
    return <img src={`http://192.168.8.188:9000/covers/${slug}.jpg`} alt={alt} {...props} />;
}


export default CoverImage;