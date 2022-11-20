import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import { Provider as ReduxProvider } from 'react-redux';
import store from 'store';
import AppRoutes from './AppRoutes';
import Theme from './Theme';
import { ShoppingCartProvider } from './context/ShoppingCartContext';

export default class App extends Component {
  static displayName = App.name;

  render() {
      return <ReduxProvider store={store}>
          <ShoppingCartProvider>
            <Theme>
                <Routes>
                  {AppRoutes.map((route, index) => {
                    const { element, ...rest } = route;
                    return <Route key={index} {...rest} element={element} />;
                  })}
                </Routes>
              </Theme>
          </ShoppingCartProvider>
      </ReduxProvider>;
  }
}
