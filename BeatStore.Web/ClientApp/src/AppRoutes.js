import { HomePage } from "./pages/HomePage";
import { ContactPage } from "./pages/ContactPage";
import { AboutUsPage } from "./pages/AboutUsPage";
import { ShoppingCartPage } from "./pages/ShoppingCartPage";

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
  }
];

export default AppRoutes;
