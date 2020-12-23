// eslint-disable-next-line @typescript-eslint/no-var-requires
const colors = require('tailwindcss/colors');

module.exports = {
  purge: ['./src/**/*.{vue,js,ts,jsx,tsx}'],
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {
      colors: {
        orange: colors.orange,
        indigo: colors.indigo,
        blueGray: colors.blueGray,
        teal: colors.teal,
        // Main color palette
        background: colors.gray['100'],
        loginBackground: colors.gray['300'],
        sidebar: colors.gray['200'],
        typo: colors.gray['700'],
        primary: colors.indigo['400'],
        primaryHover: colors.indigo['600'],
        border: colors.gray['300']
      },
      spacing: {
        100: '24rem',
        102: '26rem',
        104: '28rem',
        106: '30rem',
        108: '32rem',
        110: '34rem',
        112: '36rem',
        114: '38rem'
      },
      screens: {
        xxl: '1400px'
      }
    },
    container: {
      center: true,
      padding: '1rem'
    }
  },
  variants: {
    backgroundColor: ['responsive', 'hover', 'focus', 'active'],
    animation: ['responsive', 'motion-safe', 'motion-reduce']
  },
  plugins: [require('@tailwindcss/forms')]
};
