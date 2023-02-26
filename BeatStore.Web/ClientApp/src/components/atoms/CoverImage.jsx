import React from 'react';
import { MINIO_URL } from "Constants";

const CoverImage = ({ alt, slug, ...props }) => {
    return <img src={`${MINIO_URL}/covers/${slug}.jpg`} alt={alt} {...props} />;
}


export default CoverImage;