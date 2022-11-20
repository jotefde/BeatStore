import React, { useEffect, useState } from 'react';
import { Form } from 'react-bootstrap';
import { Inputs } from 'components/atoms';
import { GoogleRecaptcha } from 'components/molecules';
import cx from 'classnames';
import { sendContactMessage } from 'actions';
import { connect } from 'react-redux';

const ContactForm = ({ sendMessage, contactMessageResult, className, ...props }) => {
    const [isDisabled, setDisable] = useState(false);
    const [fields, setFields] = useState({
        name: '',
        subject: '',
        email: '',
        message: ''
    });

    const [errors, setErrors] = useState({
        name: '',
        subject: '',
        email: '',
        message: ''
    });

    const [captchaVerified, setCaptchaVerification] = useState(false);
    const onCaptchaVerified = verified => setCaptchaVerification(verified);

    useEffect(() => {
        const { isProcess, isDone, isSuccess, errors: newErrors } = contactMessageResult;
        if (isProcess)
            setDisable(true);
        else
            setDisable(false);

        if (isDone && !isSuccess) {
            for (let key of Object.keys(errors)) {
                if (newErrors[key])
                    errors[key] = newErrors[key];
                else
                    errors[key] = '';
            }

            setErrors(errors);
        }
        else if (isSuccess) {
            console.log("SUCCESS");
            for (let key of Object.keys(fields))
                fields[key] = '';
            for (let key of Object.keys(errors))
                errors[key] = '';
            setFields(fields);
            setErrors(errors);

        }
    }, [contactMessageResult]);

    const handleInputChange = e => {
        if (isDisabled)
            return;
        const { name, value } = e.target;
        if (fields[name] !== value) {
            setFields(prevState => ({
                ...prevState,
                [name]: value
            }));
        }
    };

    const handleSubmit = e => {
        e.preventDefault();
        if (!isDisabled && captchaVerified)
            sendMessage(fields);
    }

    const FormField = (name, label, placeholder, type) => (
        <Form.Group className="position-relative contactFormInput">
            <Form.Label>{label}</Form.Label>
            <Form.Control
                id={`ContactInput_${name}`}
                name={name}
                type={type}
                as={type === 'textarea' ? 'textarea' : 'input'}
                value={fields[name]}
                onChange={handleInputChange}
                placeholder={placeholder}
                isInvalid={errors[name]?.length > 0}
                disabled={isDisabled}
            />
            <Form.Control.Feedback type="invalid" tooltip>
                {errors[name]}
            </Form.Control.Feedback>
        </Form.Group>
    );

    return (
        <article className={cx('contactFormWrapper', className)} {...props}>
            <header className={'contactFormWrapper__header'}>
                <h3>Write to Us</h3>
                {'Our team will be happy to answer any questions you may have.'}
            </header>
            <Form id={'ContactForm'} className={'contactFormWrapper__content'}
                validated={false}
                onSubmit={handleSubmit}>

                {FormField('name', 'Full Name', 'John Doe', 'text')}
                {FormField('email', 'E-mail', 'example@domain.com', 'email')}
                {FormField('subject', 'Subject', 'Subject of message...', 'text')}
                {FormField('message', 'Message', 'Content of your message...', 'textarea')}

                <Form.Floating>
                    <GoogleRecaptcha onVerified={onCaptchaVerified} />
                </Form.Floating>

                <Inputs.PrimaryButton variant="primary" type="submit"
                    disabled={isDisabled || !captchaVerified}>
                    {'Wyślij'}
                </Inputs.PrimaryButton>
            </Form>
        </article>
    );
};

const mapDispatchToProps = dispatch => ({
    sendMessage: (fields) => dispatch(sendContactMessage(fields))
})

const mapStateToProps = ({ contactMessageResult }) => ({
    contactMessageResult: contactMessageResult || {
        isProcess: false,
        isDone: false,
        isSuccess: false,
        errors: []
    }
});

export default connect(mapStateToProps, mapDispatchToProps)(ContactForm);