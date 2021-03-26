module.exports = {
  purge: ['./src/**/*.{js,jsx,ts,tsx}', './public/index.html'],
  darkMode: 'class', // or 'media' or 'class'
  theme: {
    extend: {
      colors: {
        primary: '#00a6ed',
        secondary: '#ffb400',
        active: '#7fb800',
        back: '#ececec'
      }
    },
  },
  variants: {
    extend: {
      width: ['focus-within'],
      height: ['focus-within'],
      position: ['focus-within'],
      padding: ['focus-within'],
      lineHeight: ['hover', 'focus'],
      fontSize: ['hover', 'focus'],
    },
  },
  plugins: [],
}
