import React from 'react';
import { useNavigate } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import cx from 'classnames';

const PrimaryButton = ({ route, className, children, ...props }) => {
  const navigate = useNavigate();
  return (
    <Button className={ cx('primaryButton', className) } onClick={() => route && navigate(route)} { ...props }>{ children }</Button>
  );
};

export default PrimaryButton;