module.exports = {
  root: true,
  env: {
    browser: true,
    node: true
  },
  parserOptions: {
    parser: '@typescript-eslint/parser',
    sourceType: 'module', // allow the use of imports statements
    ecmaVersion: 2020
  },
  extends: [
    'plugin:vue/vue3-strongly-recommended',
    'plugin:@typescript-eslint/recommended',
    '@vue/typescript/recommended',
    '@vue/prettier',
    '@vue/prettier/@typescript-eslint'
  ],
  plugins: ['@typescript-eslint'],
  ignorePatterns: ['node_modules/**', 'dist/**', '**/*.svg', '**/svgs/*.vue'],
  rules: {
    'no-console': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'no-debugger': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'linebreak-style': ['error', 'unix'],
    // TS
    'no-useless-constructor': 'off',
    '@typescript-eslint/semi': ['warn', 'always'],
    '@typescript-eslint/member-delimiter-style': [
      'error',
      { multiline: { delimiter: 'semi', requireLast: true } }
    ],
    '@typescript-eslint/type-annotation-spacing': ['error', {}],

    'no-unused-vars': 'off',
    '@typescript-eslint/no-unused-vars': [2, { args: 'none', ignoreRestSiblings: true }],
    'no-redeclare': 'off',
    '@typescript-eslint/no-redeclare': 'error',
    // off
    '@typescript-eslint/explicit-function-return-type': 'off',
    '@typescript-eslint/explicit-member-accessibility': 'off',
    '@typescript-eslint/no-explicit-any': 'off',
    '@typescript-eslint/no-parameter-properties': 'off',
    '@typescript-eslint/no-empty-interface': 'off',
    '@typescript-eslint/ban-ts-ignore': 'off',
    '@typescript-eslint/no-empty-function': 'off',
    '@typescript-eslint/no-non-null-assertion': 'off',
    '@typescript-eslint/ban-ts-comment': 'off',
    '@typescript-eslint/explicit-module-boundary-types': 'off',
    '@typescript-eslint/ban-types': 'off',
    // es6
    'no-var': 'error',
    'prefer-const': [
      'error',
      {
        destructuring: 'any',
        ignoreReadBeforeAssign: true
      }
    ],
    'prefer-arrow-callback': [
      'error',
      {
        allowNamedFunctions: false,
        allowUnboundThis: true
      }
    ],
    'object-shorthand': [
      'error',
      'always',
      {
        ignoreConstructors: false,
        avoidQuotes: true
      }
    ],
    'template-curly-spacing': 'error',
    'arrow-parens': ['error', 'always', { requireForBlockBody: true }],
    'generator-star-spacing': 'off',
    'comma-dangle': ['error', 'never'],
    // best-practice
    'array-callback-return': 'error',
    'block-scoped-var': 'error',
    'consistent-return': 'off',
    complexity: ['off', 11],
    eqeqeq: ['error', 'allow-null'],
    'no-alert': 'warn',
    'no-case-declarations': 'error',
    'no-multi-spaces': 'error',
    'no-multi-str': 'error',
    'no-with': 'error',
    'no-void': 'error',
    'no-useless-escape': 'error',
    'vars-on-top': 'error',
    'require-await': 'off',
    'no-return-assign': 'off',
    'operator-linebreak': [2, 'before']

    // 'import/prefer-default-export': 'off',
    // 'max-len': ['error', { code: 170, tabWidth: 2, ignoreComments: true, ignoreTrailingComments: true }],
    // 'lines-between-class-members': ['error', 'always', { exceptAfterSingleLine: true }],
    // 'object-curly-newline': [
    //   'error',
    //   { ImportDeclaration: { multiline: true, minProperties: 10 }, ExportDeclaration: 'never' }
    // ],
    // 'no-bitwise': ['error', { allow: ['~', '<<', '|'] }],
    // 'import/no-cycle': 0,
    // 'no-param-reassign': [2, { props: false }],
    // 'arrow-body-style': ['error', 'as-needed'],
    // 'implicit-arrow-linebreak': 'off',
    // 'function-paren-newline': ['error', 'consistent'],
  }
};
