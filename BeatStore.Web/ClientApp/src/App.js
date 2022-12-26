import React, {Component} from 'react';
import {Route, Routes} from 'react-router-dom';
import AppRoutes from './AppRoutes';
import Theme from './Theme';
import {ShoppingCartProvider} from './context/ShoppingCartContext';
import {QueryClient, QueryClientProvider} from "@tanstack/react-query";

const App = () => {
    const queryClient = new QueryClient()
    return <QueryClientProvider client={queryClient}>
            <ShoppingCartProvider>
                <Theme>
                    <Routes>
                        {AppRoutes.map((route, index) => {
                            const {element, ...rest} = route;
                            return <Route key={index} {...rest} element={element}/>;
                        })}
                    </Routes>
                </Theme>
            </ShoppingCartProvider>
        </QueryClientProvider>;
}

export default App;