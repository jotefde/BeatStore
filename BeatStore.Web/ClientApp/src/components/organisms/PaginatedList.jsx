import React from 'react';
import cx from 'classnames';
import { Container, Row } from 'react-bootstrap';
import { usePagination } from 'hooks/Pagination';

const PaginatedList = ({ items, pageCap, className, ...props }) => {
  const [ activeItems, menu ] = usePagination(items, pageCap);
  const mappedItems = activeItems.map((item, index) => {
    return <article key={`item-${index}`}>{ item }</article>
  });
  return (
    <Container as={'section'} className={cx('paginatedList', className)} {...props}>
      <Row className={'justify-content-center'}>
        { menu }
      </Row>
      <Row className={'justify-content-center'}>
        { mappedItems }
      </Row>
      <Row className={'justify-content-center'}>
        { menu }
      </Row>
    </Container>
  );
};

export default PaginatedList;