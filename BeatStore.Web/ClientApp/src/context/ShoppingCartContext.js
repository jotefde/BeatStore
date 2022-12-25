import React, { useContext, useEffect, useState } from 'react';

const ShoppingCartContext = React.createContext({});

export const useShoppingCart = () => useContext(ShoppingCartContext);
export const ShoppingCartProvider = ({ children }) => {
    const [cartItems, setCartItems] = useState([]);
    let init = true;

    useEffect(() => {
        if (cartItems.length === 0 && init) {
            let cart = localStorage.getItem('shoppingCart');
            setCartItems(JSON.parse(cart || '[]'));
        }
        init = false;
    }, [init]);

    useEffect(() => {
        localStorage.setItem("shoppingCart", JSON.stringify(cartItems))
    }, [cartItems])

    const getQuantity = () => cartItems.length;

    const getItem = (id) => cartItems.find((item) => item.Id === id);

    const addItem = (Id, Name, Slug, Price, Description) => {
        setCartItems(currItems => {
            if (getItem(Id) == null)
                return [...currItems, { Id, Name, Slug, Price, Description }];
            else
                return currItems;
        });
    };

    const removeItem = (id) => {
        setCartItems(currItems => {
            return currItems.filter(item => item.Id !== id);
        });
    };

    const getTotalPrice = () => {
        let total = 0;
        for (const track of cartItems) {
            total += track.Price;
        }
        return total;
    };

    const clear = () => {
        setCartItems([]);
    }

    return <ShoppingCartContext.Provider
        value={{
            getQuantity,
            getTotalPrice,
            getItem,
            addItem,
            removeItem,
            clear,
            items: cartItems
        }}
    >
        {children}
    </ShoppingCartContext.Provider>;
}