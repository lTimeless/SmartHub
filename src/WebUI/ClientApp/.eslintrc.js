module.exports = {
  root: true,
  env: {
    node: true
  },
  parserOptions: {
    parser: '@typescript-eslint/parser',
    sourceType: 'module', // allow the use of imports statements
    ecmaVersion: 2020
  },
  extends: [
    'plugin:vue/vue3-recommended',
    'eslint:recommended',
    '@vue/typescript/recommended',
    // 'prettier',
    // 'prettier/vue',
    // 'prettier/@typescript-eslint'
    '@vue/prettier',
    '@vue/prettier/@typescript-eslint'
  ],
  plugins: ['@typescript-eslint', 'prettier'],
  rules: {
    'no-console': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'no-debugger': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'linebreak-style': ['error', 'unix'],
    // TS
    '@typescript-eslint/semi': ['warn', 'always'],
    '@typescript-eslint/member-delimiter-style': [
      'error',
      {multiline: {delimiter: 'semi', requireLast: true}}
    ],
    '@typescript-eslint/type-annotation-spacing': ['error', {}],
    '@typescript-eslint/no-use-before-define': ['error', {functions: false, classes: true, variables: false}],

    // off
    '@typescript-eslint/explicit-function-return-type': 'off',
    '@typescript-eslint/explicit-member-accessibility': 'off',
    '@typescript-eslint/explicit-module-boundary-types': 'off',
    '@typescript-eslint/no-explicit-any': 'off',
    '@typescript-eslint/no-parameter-properties': 'off',
    '@typescript-eslint/ban-ts-ignore': 'off',
    '@typescript-eslint/no-non-null-assertion': 'off',
    '@typescript-eslint/ban-ts-comment': 'off',
    // es6
    'template-curly-spacing': 'error',
    'arrow-parens': ['error', 'always', { requireForBlockBody: true }],
    'comma-dangle': ['error', 'never'],
    'no-use-before-define': 'off',
    // best-practice
    'array-callback-return': 'error',
    'block-scoped-var': 'error',
    'consistent-return': 'off',
    complexity: ['off', 11],
    eqeqeq: ['error', 'allow-null'],
    'vars-on-top': 'warn',
    'operator-linebreak': [2, 'before'],
    // vue
    'vue/max-attributes-per-line': ['warn', { singleline: 6 }],
    'vue/singleline-html-element-content-newline': 'off',
    'vue/html-self-closing': [
      'error',
      {
        html: {
          void: 'always',
          normal: 'always',
          component: 'always'
        },
        svg: 'always',
        math: 'always'
      }
    ]
  },
  ignorePatterns: ['node_modules/', 'dist/', 'generated/', '!.*', 'schema.graphql', '**/*.svg']
};
