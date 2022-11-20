import React, { useState } from 'react';
import ReCAPTCHA from 'react-google-recaptcha';
import cx from 'classnames';

const GoogleRecaptcha = ({ onVerified, classNames, ...props }) => {

  const onChange = value => {
      onVerified(typeof value === 'string' && value.length > 0);
  };

  return <ReCAPTCHA
    sitekey="6LesbjAeAAAAAL0FSyhiChOziHgQIAFC0tfS4Da1"
    onChange={onChange}
    className={cx('recaptcha-input', classNames)}
    { ...props }
  />
}

export default GoogleRecaptcha;