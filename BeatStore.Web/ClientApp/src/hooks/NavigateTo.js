import React from 'react';
import { useNavigate } from 'react-router-dom';

const NavigateTo = (path, preventDefault = true) =>
{
    const navigate = useNavigate();
    const onClick = e => {
        if (preventDefault)
            e.preventDefault();
        navigate(path);
    }
    return onClick;
};

export default NavigateTo;