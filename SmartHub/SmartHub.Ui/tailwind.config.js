module.exports = {
  purge: { content: ['./public/**/*.html', './src/**/*.vue'] },
  darkMode: false, // or 'media' or 'class'
  theme: {
    boxShadow: {
      xs: '0 0 0 1px rgba(0, 0, 0, 0.05)',
      sm: '0 1px 2px 0 rgba(0, 0, 0, 0.05)',
      default: '0 1px 3px 0 rgba(0, 0, 0, 0.1), 0 1px 2px 0 rgba(0, 0, 0, 0.06)',
      md: '0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06)',
      lg: '0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05)',
      xl: '0 20px 25px -5px rgba(0, 0, 0, 0.1), 0 10px 10px -5px rgba(0, 0, 0, 0.04)',
      '2xl': '0 25px 50px -12px rgba(0, 0, 0, 0.25)',
      '3xl': '0 35px 60px -15px rgba(0, 0, 0, 0.3)',
      inner: 'inset 0 2px 4px 0 rgba(0, 0, 0, 0.06)',
      outline: '0 0 0 3px rgba(66, 153, 225, 0.5)',
      outlineIndigo: '0 0 0 3px rgba(90, 103, 216, 0.5)',
      focus: '0 0 0 3px rgba(66, 153, 225, 0.5)',
      none: 'none'
    },
    extend: {
      colors: {
        ui: {
          background: 'var(--color-ui-background)',
          loginBackground: 'var(--color-ui-login-background)',
          sidebar: 'var(--color-ui-sidebar)',
          typo: 'var(--color-ui-typo)',
          primary: 'var(--color-ui-primary)',
          primaryHover: 'theme(colors.indigo.700)',
          border: 'var(--color-ui-border)'
        }
      },
      spacing: {
        sm: '24rem'
      },
      screens: {
        xxl: '1400px'
      }
    },
    container: {
      center: true,
      padding: '1rem'
    }
    // customForms: (theme) => ({
    //   default: {
    //     input: {
    //       borderRadius: theme('borderRadius.lg'),
    //       '&:focus': {
    //         borderColor: theme('colors.indigo.600')
    //       }
    //     }
    //   }
    // })
  },
  variants: {
    backgroundColor: ['responsive', 'hover', 'focus', 'active'],
    animation: ['responsive', 'motion-safe', 'motion-reduce']
  },
  plugins: [require('@tailwindcss/forms')]
};
