import React from 'react';
import { Inputs } from 'components/atoms';

const ToOfferButton = (props) => {
  return <Inputs.PrimaryButton route={'/produkty'} { ...props }>Pełna oferta</Inputs.PrimaryButton>
};

export default ToOfferButton;