/* eslint-disable @typescript-eslint/no-var-requires */
const colors = require('tailwindcss/colors');
const typography = require('@tailwindcss/typography');
const forms = require('@tailwindcss/forms');

module.exports = {
  purge: {
    enabled: process.env.NODE_ENV === 'production',
    content: ['./index.html', './public/**/*.html', './src/**/*.{js,jsx,ts,tsx,vue,md}'],
    options: {
      safelist: ['prose', 'prose-sm', 'm-auto']
    }
  },
  mode: 'jit',
  darkMode: 'media', // or 'media' or 'class'
  theme: {
    extend: {
      colors: {
        orange: colors.orange,
        indigo: colors.indigo,
        blueGray: colors.blueGray,
        teal: colors.teal,
        // Primary colors
        BlueGreen: '#135872', // just for later
        primaryBlue: '#264653',
        primaryBlueHover: '#B9D4DF',
        primaryGreen: '#2A9D8F',
        primaryGreenHover: '#8EE1D7',
        primaryYellow: '#E9c46a',
        primaryYellowHover: '#F2DCA6',
        primaryBrown: '#f4a261',
        primaryBrownHover: '#f8c8a0',
        primarySienna: '#e76f51',
        primarySiennaHover: '#f3b5a5',
        // Color palette
        charcoalBlue: {
          50: '#D5E5EC',
          100: '#B9D4DF',
          200: '#9DC3D2',
          300: '#81B2C5',
          400: '#65A1B8',
          500: '#4D8EA8',
          600: '#40768C',
          700: '#335F70',
          800: '#264653', // primary
          900: '#1a2f38'
        },
        persianGreen: {
          50: '#CFF2EE',
          100: '#AEEAE3',
          200: '#8EE1D7',
          300: '#6ED8CC',
          400: '#4ED0C1',
          500: '#33C1B1',
          600: '#2A9D8F', // primary
          700: '#228176',
          800: '#1A6158',
          900: '#11403B'
        },
        crayolaYellow: {
          50: '#F7EACA',
          100: '#F2DCA6',
          200: '#ECCE83',
          300: '#E9c46a', // primary
          400: '#e4ba4e',
          500: '#dfac2a',
          600: '#c3941d',
          700: '#a07918',
          800: '#7c5e13',
          900: '#59430d'
        },
        sandyBrown: {
          50: '#fbdec6',
          100: '#f8c8a0',
          200: '#f7bc8d',
          300: '#f4a261', // primary
          400: '#f29040',
          500: '#ef7a1a',
          600: '#d2660f',
          700: '#ac540c',
          800: '#864109',
          900: '#5f2f07'
        },
        burntSienna: {
          50: '#f8d2c9',
          100: '#f3b5a5',
          200: '#ee9781',
          300: '#e76f51', // primary
          400: '#e45c3a',
          500: '#d7431d',
          600: '#b43718',
          700: '#902c14',
          800: '#6c210f',
          900: '#48160a'
        }
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
    // extend: {
    //   ringColor: ['hover'],
    //   ringOffsetColor: ['hover'],
    //   ringOffsetWidth: ['hover'],
    //   ringOpacity: ['hover'],
    //   ringWidth: ['hover'],
    //   opacity: ['disabled'],
    //   scale: ['hover']
    // }
  },
  plugins: [forms, typography]
};
