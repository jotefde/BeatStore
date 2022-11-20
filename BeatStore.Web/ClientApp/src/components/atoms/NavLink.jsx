import React from 'react';
import { NavLink as ReactNL } from 'react-router-dom';
import cx from 'classnames';

const NavLink = ({ to, className, children, ...props }) => {
  const classNames = cx('navLink', className);
  if(to.includes('://'))
    return <a href={ to } className={ classNames } { ...props }>{ children }</a>;
  else
    return <ReactNL to={ to } className={ classNames } { ...props }>{ children }</ReactNL>;
};

export default NavLink;