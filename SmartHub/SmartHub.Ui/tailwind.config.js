module.exports = {
  purge: ['./src/**/*.html', './src/**/*.vue', './src/**/*.jsx'],
  theme: {
    extend: {
      colors: {
        ui: {
          background: 'var(--color-ui-background)',
          loginBackground: 'var(--color-ui-login-background)',
          sidebar: 'var(--color-ui-sidebar)',
          typo: 'var(--color-ui-typo)',
          primary: 'var(--color-ui-primary)',
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
    },
    customForms: (theme) => ({
      checkbox: {
        borderColor: theme('colors.gray.900'),
        color: theme('colors.gray.900')
      }
    })
  },
  variants: {
    backgroundColor: ['responsive', 'hover', 'focus', 'active'],
    animation: ['responsive', 'motion-safe', 'motion-reduce']
  },
  plugins: [require('@tailwindcss/custom-forms')]
};
