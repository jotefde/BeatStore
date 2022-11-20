import React from 'react';
import cx from 'classnames';

const MailLink = ({ mail, className, ...props }) => <a href={`maito:${mail}`} className={ cx('mailLink', className) } {...props}>{mail}</a>;

export default MailLink;