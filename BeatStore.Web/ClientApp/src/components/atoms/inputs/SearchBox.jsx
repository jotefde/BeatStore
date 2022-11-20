import React, { useRef } from 'react';
import { Form, InputGroup, Col, FormControl, Row } from 'react-bootstrap';
import { FaSearch } from 'react-icons/fa';
import { useNavigate } from 'react-router-dom';

const SearchBox = (props) => {
  const searchInput = useRef(null);
  const navigate = useNavigate();
  const searchHandler = (e) => {
    e.preventDefault();
    const { value } = searchInput?.current ?? { value: false };
    if(!value?.length)
      return false;
    navigate(`/search/${value}`);
    return false;
  };

  return <Form id={'SearchBox'} {...props} onSubmit={searchHandler}>
    <InputGroup className="mb-2">
      <InputGroup.Text>
        <FaSearch color={'rgb(130, 130, 130)'} title={'Search'}/>
      </InputGroup.Text>
      <FormControl ref={searchInput} id="inlineFormInputGroup" placeholder="Szukane frazy.." />
    </InputGroup>
    <InputGroup className="mb-2">
      <FormControl type={'submit'} value={'Szukaj'}/>
    </InputGroup>
  </Form>
}

export default SearchBox;