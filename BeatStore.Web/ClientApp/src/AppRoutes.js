import { HomePage } from "./pages/HomePage";
import { ContactPage } from "./pages/ContactPage";
import { AboutUsPage } from "./pages/AboutUsPage";
import ShoppingCartPage from "./pages/ShoppingCartPage";
import TrackPage from "./pages/TrackPage";
import CustomerOrderPage from "./pages/CustomerOrderPage";

const AppRoutes = [
  {
    index: true,
    element: <HomePage />
  },
  {
    path: '/contact',
    element: <ContactPage />
  },
  {
    path: '/about-us',
    element: <AboutUsPage />
  },
  {
    path: '/shopping-cart',
    element: <ShoppingCartPage />
  },
  {
      path: '/product/:slug',
      element: <TrackPage />
  },
  {
      path: '/order/:accessKey/show',
      element: <CustomerOrderPage />
  }
];

export default AppRoutes;
