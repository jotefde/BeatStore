import React from 'react';
import cx from 'classnames';
import { Col, Container, Row } from 'react-bootstrap';
import { usePagination } from 'hooks/Pagination';

const PaginatedGrid = ({ items, pageCap, className, ...props }) => {
  const [ activeItems, menu ] = usePagination(items, pageCap);
  const mappedItems = activeItems.map((item, index) => {
    return <Col sm={ 12 } md={ 6 } xl={4} key={`item-${index}`}>{ item }</Col>
  });
  return (
    <Container as={'section'} className={cx('paginatedGrid', className)} {...props}>
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

export default PaginatedGrid;