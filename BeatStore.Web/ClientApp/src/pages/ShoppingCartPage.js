import React, {useEffect, useState} from 'react';
import { Col, Container, Row, Button, Form, InputGroup } from 'react-bootstrap';
import {ShoppingCartPreview} from "components/organisms";
import { usePostNewOrder } from "actions";
import cx from 'classnames';
import {useShoppingCart} from "context/ShoppingCartContext";
import ClipLoader from "react-spinners/ClipLoader";

const ShoppingCartPage = () => {
    const shoppingCart = useShoppingCart();
    const [errors, setErrors] = useState({});
    const initValues = {
        Description: '',
        CustomerEmail: '',
        CustomerEmailRepeat: '',
        CustomerPhone: '',
        CustomerFirstName: '',
        CustomerLastName: ''
    };
    const [values, setValues] = useState(initValues);

    const { isLoading, isSuccess, isError, mutate, error, data, reset } = usePostNewOrder();


    useEffect(() => {

        if(isSuccess)
        {
            setErrors({});
            setValues(initValues);
            shoppingCart.clear();
            window.open(data, '_blank');
            reset();
        }
        else if(isError)
        {
            if(Array.isArray(error))
            {
                setErrors(error);
            }
            else {
                const newErrors = {};
                for (const [key, value] of Object.entries(error)) {
                    newErrors[key] = value[0];
                }
                setErrors(newErrors);
            }
            reset();
        }
    }, [isSuccess, isError])


    const handleSubmit = (e) => {
        const form = e.currentTarget;
        e.preventDefault();
        e.stopPropagation();

        if(values.CustomerEmail != values.CustomerEmailRepeat)
        {
            setErrors({
                ...errors,
                CustomerEmailRepeat: 'Email does not match'
            })
            return;
        }

        if(shoppingCart.getQuantity() < 1)
        {
            alert("You have to add any track to order!")
            return;
        }

        const newValues = {...values};
        delete newValues.CustomerEmailRepeat;

        mutate({
            ...newValues,
            CurrencyCode: 'PLN',
            Items: shoppingCart.items.map(item => item.Id)
        });
    };

    const changeInput = (e) => {
        const input = e.currentTarget;
        setValues({
            ...values,
            [input.id]: input.value
        });
    };

    return (
        <Container as={'article'} id={'ShoppingCart'}>
            <h1 className={'pageTitle'}>Shopping cart</h1>
            <Row className={'justify-content-center'}>
                <Col xs={{span: 12, order: 2}} lg={{span: 6, order: 1}}>
                    <Form onSubmit={handleSubmit} className={'orderForm'}>
                        <Row className="mb-3">
                            <Form.Group as={Col} xs={12} md={6} controlId="CustomerFirstName">
                                <Form.Label>First name*</Form.Label>
                                <InputGroup>
                                    <Form.Control
                                        type="text"
                                        placeholder="First name"
                                        aria-describedby="inputGroupPrepend"
                                        isInvalid={errors.CustomerFirstName}
                                        value={values.CustomerFirstName}
                                        onChange={changeInput}
                                        required
                                    />
                                    <Form.Control.Feedback type="invalid">
                                        {errors.CustomerFirstName}
                                    </Form.Control.Feedback>
                                </InputGroup>
                            </Form.Group>
                            <Form.Group as={Col} xs={12} md={6} controlId="CustomerLastName">
                                <Form.Label>Last name*</Form.Label>
                                <InputGroup>
                                    <Form.Control
                                        type="text"
                                        placeholder="Last name"
                                        aria-describedby="inputGroupPrepend"
                                        isInvalid={errors.CustomerLastName}
                                        value={values.CustomerLastName}
                                        onChange={changeInput}
                                        required
                                    />
                                    <Form.Control.Feedback type="invalid">
                                        {errors.CustomerLastName}
                                    </Form.Control.Feedback>
                                </InputGroup>
                            </Form.Group>
                        </Row>

                        <Row className="mb-5">
                            <Form.Group as={Col} xs={12} md={6} controlId="CustomerEmail">
                                <Form.Label>Email*</Form.Label>
                                <InputGroup>
                                    <InputGroup.Text id="inputGroupPrepend">@</InputGroup.Text>
                                    <Form.Control
                                        type="text"
                                        placeholder="Email"
                                        aria-describedby="inputGroupPrepend"
                                        isInvalid={errors.CustomerEmail}
                                        value={values.CustomerEmail}
                                        onChange={changeInput}
                                        required
                                    />
                                    <Form.Control.Feedback type="invalid">
                                        {errors.CustomerEmail}
                                    </Form.Control.Feedback>
                                </InputGroup>
                            </Form.Group>
                            <Form.Group as={Col} xs={12} md={6} controlId="CustomerEmailRepeat">
                                <Form.Label>Repeat email*</Form.Label>
                                <InputGroup>
                                    <InputGroup.Text id="inputGroupPrepend">@</InputGroup.Text>
                                    <Form.Control
                                        type="email"
                                        placeholder="Repeat email"
                                        aria-describedby="inputGroupPrepend"
                                        isInvalid={errors.CustomerEmailRepeat}
                                        value={values.CustomerEmailRepeat}
                                        onChange={changeInput}
                                        required
                                    />
                                    <Form.Control.Feedback type="invalid">
                                        {errors.CustomerEmailRepeat}
                                    </Form.Control.Feedback>
                                </InputGroup>
                            </Form.Group>
                        </Row>

                        <Row className="mb-3">
                            <Form.Group as={Col} controlId="CustomerPhone">
                                <Form.Label>Phone number</Form.Label>
                                <InputGroup>
                                    <Form.Control
                                        type="phone"
                                        placeholder="Phone number"
                                        aria-describedby="inputGroupPrepend"
                                        isInvalid={errors.CustomerPhone}
                                        value={values.CustomerPhone}
                                        onChange={changeInput}
                                    />
                                    <Form.Control.Feedback type="invalid">
                                        {errors.CustomerPhone}
                                    </Form.Control.Feedback>
                                </InputGroup>
                            </Form.Group>
                        </Row>

                        <Row className="mb-5">
                            <Form.Group as={Col} controlId="Description">
                                <Form.Label>Description</Form.Label>
                                <InputGroup>
                                    <Form.Control
                                        as="textarea" rows={3}
                                        placeholder="Additional message to order"
                                        aria-describedby="inputGroupPrepend"
                                        isInvalid={errors.Description}
                                        value={values.Description}
                                        onChange={changeInput}
                                    />
                                    <Form.Control.Feedback type="invalid">
                                        {errors.Description}
                                    </Form.Control.Feedback>
                                </InputGroup>
                            </Form.Group>
                        </Row>

                        <Row className={cx('mb-3', 'justify-content-center')}>
                            <Form.Group as={Col} md={6} controlId="PayMethod">
                                <Form.Label>Payment method*</Form.Label>
                                <Form.Select
                                    value='PayU'>
                                    <option value="PayU">PayU Online transfer</option>
                                   {/* <option value="CARD_TOKEN">Credit card</option>*/}
                                </Form.Select>
                            </Form.Group>
                        </Row>

                        <Row className={cx('mb-3', 'justify-content-center')}>
                            <Form.Group as={Col} xs={12}>
                                <Form.Check
                                    required
                                    label="Agree to terms and conditions*"
                                    feedback="You must agree before submitting."
                                    feedbackType="invalid"
                                />
                            </Form.Group>
                        </Row>

                        <Row className={cx('mb-3', 'justify-content-center')}>
                            <Button variant={'success'} disabled={shoppingCart.getQuantity() < 1 || isLoading} type="submit">
                                { isLoading ?
                                    <ClipLoader
                                        color={'white'}
                                        size={25}
                                        aria-label="Loading Spinner"
                                        data-testid="loader"
                                    /> :
                                    'Confirm and pay'
                                }

                            </Button>
                            <output>
                                {errors[0] && <p>Error: "{errors[0]}"</p>}
                            </output>
                        </Row>
                    </Form>
                </Col>
                <Col xs={{span: 12, order: 1}} lg={{span: 6, order: 2}}>
                    <ShoppingCartPreview className={'shoppingCartPreview'} />
                </Col>
            </Row>
        </Container>
    );
}

export default ShoppingCartPage;