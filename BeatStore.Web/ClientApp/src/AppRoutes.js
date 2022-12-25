import { HomePage } from "./pages/HomePage";
import { ContactPage } from "./pages/ContactPage";
import { AboutUsPage } from "./pages/AboutUsPage";
import ShoppingCartPage from "./pages/ShoppingCartPage";
import TrackPage from "./pages/TrackPage";

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
  }
];

export default AppRoutes;
