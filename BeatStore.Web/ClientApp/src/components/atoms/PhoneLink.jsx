import React from 'react';
import cx from 'classnames';

const PhoneLink = ({ number, className, ...props }) => <a href={`tel:${number}`} className={ cx('phoneLink', className) } {...props}>{number}</a>;

export default PhoneLink;