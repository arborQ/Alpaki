import i18n from "i18next";
import { initReactI18next } from "react-i18next";

// the translations
// (tip move them in a JSON file and import them)
const resources = {
    en: {
        translation: {
            "Welcome to React": "Welcome to React and react-i18next",
            "login.login_label": "User name",
            "login.password_label": "Password",
            "login.submit": "Validate user",
            "menu.home": "Dashboard",
            "menu.login": "Sign in!",
            "menu.shop.products": "Products",
            "menu.shop.products.ai": "Products (AI)",
        }
    },
    pl: {
        translation: {
            "Welcome to React": "Bienvenue à React et react-i18next",
            "login.login_label": "Nazwa użytkownika",
            "login.password_label": "Hasło",
            "login.submit": "Zaloguj mnie",
            "menu.home": "Dashboard",
            "menu.login": "Zaloguj!",
            "menu.shop.products": "Produkty",
            "menu.shop.products.ai": "Produkty (AI)",
        }
    }
};

i18n
    .use(initReactI18next) // passes i18n down to react-i18next
    .init({
        resources,
        lng: "en",

        keySeparator: false, // we do not use keys in form messages.welcome

        interpolation: {
            escapeValue: false // react already safes from xss
        }
    });

export default i18n;