import React, { useState } from 'react';
import cx from 'classnames';
import { Form } from 'react-bootstrap';
import { Inputs } from 'components/atoms';

const handleSubmit = () => null;

const SearchForm = ({ id, className, ...props }) => {
    const [searchTextValue, onSearchTextChange] = useState('');
    return <Form id={id} className={cx('searchForm', className)} { ...props }
              validated={false}
              onSubmit={handleSubmit}>
          <Form.Group className="position-relative searchFormInput">
              <Form.Label>Search for track</Form.Label>
              <Form.Control
                    id={`SearchInput_searchText`}
                  name={'searchText'}
                  type={'text'}
                  as={'input'}
                  value={searchTextValue}
                  onChange={onSearchTextChange}
                  placeholder={'Enter a text...'}
              />
          </Form.Group>

        <Inputs.PrimaryButton variant="primary" type="submit">
            {'Search'}
        </Inputs.PrimaryButton>
      </Form>
};

export default SearchForm;