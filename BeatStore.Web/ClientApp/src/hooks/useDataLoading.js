import ClipLoader from "react-spinners/ClipLoader";
import React from "react";

const useDataLoading = (isLoading, errors) => {
    if(errors)
    {
        console.log(errors)
        if(Array.isArray(errors) && errors.length == 1)
            return <h2>{errors[0]}</h2>;
        return <></>;
    }

    if(isLoading)
        return <div
            style={{ margin: '0 auto', display: 'block' }}>
            <ClipLoader
                color={'purple'}
                size={150}
                aria-label="Loading Spinner"
                data-testid="loader"
            />
        </div>;
    return false;
}

export default useDataLoading;