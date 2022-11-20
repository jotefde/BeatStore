import React, { useEffect, useState } from 'react';
import { Col } from 'react-bootstrap';
import { FaAngleDoubleLeft, FaAngleDoubleRight, FaAngleLeft, FaAngleRight } from 'react-icons/fa';
import cx from 'classnames';

export const usePagination = (items, pageCap) =>
{
  const [ activePage, setActivePage ] = useState(0);
  const pageCount = Math.ceil(items.length / pageCap);
  let pageNumbers = Array(pageCount).fill(0).map((_, i)=> i+1);

  const startItem = (activePage ) * pageCap;
  const endItem = startItem + pageCap;
  const activeItems = items.slice(startItem, endItem);

  useEffect(() => setActivePage(0), [ items ]);

  let firstPage = 0;
  if(activePage > 2 && activePage+2 <= pageCount-1)
    firstPage = activePage - 2;
  else if( activePage > 2 && activePage + 2 > pageCount-1)
    firstPage = pageCount - 1 - 4;

  let lastPage = 4;
  if(activePage > 2 && activePage+2 <= pageCount-1)
    lastPage = activePage + 2;
  else if( activePage > 2 && activePage + 2 > pageCount-1)
    lastPage = pageCount-1;

  pageNumbers = pageNumbers.slice(firstPage, lastPage + 1);

  const handleFirstPage = () => setActivePage(0);
  const handlePrevPage = () => activePage > 0 && setActivePage(activePage-1);
  const handleSetPage = (index) => (index >= 0 && index < pageCount) && setActivePage(index);
  const handleNextPage = () => activePage < pageCount-1 && setActivePage(activePage+1);
  const handleLastPage = () => setActivePage(pageCount - 1);

  const pageControls = [
    <span className={'paginationMenu__control'} onClick={handleFirstPage} key={'page-first'}><FaAngleDoubleLeft /></span>,
    <span className={'paginationMenu__control'} onClick={handlePrevPage} key={'page-prev'}><FaAngleLeft /></span>
  ];
  pageControls.push(... pageNumbers.map(page => <span className={cx('paginationMenu__control', page-1 === activePage && 'paginationMenu__control--active')} key={`page-${page}`} onClick={() => handleSetPage(page-1)}>{ page }</span>) );
  pageControls.push(...[
    <span className={'paginationMenu__control'} onClick={handleNextPage} key={'page-next'}><FaAngleRight /></span>,
    <span className={'paginationMenu__control'} onClick={handleLastPage} key={'page-last'}><FaAngleDoubleRight /></span>
  ])

  const paginationMenu = <Col>
    <nav className={'paginationMenu'}>
      { pageControls }
    </nav>
  </Col>;

  return [ activeItems, paginationMenu ];
};