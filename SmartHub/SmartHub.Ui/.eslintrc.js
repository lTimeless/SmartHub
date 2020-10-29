module.exports = {
  root: true,
  env: {
    node: true
  },
  extends: [
    'plugin:vue/vue3-strongly-recommended',
    'eslint:recommended',
    'plugin:@typescript-eslint/recommended',
    '@vue/typescript/recommended',
    '@vue/prettier',
    '@vue/prettier/@typescript-eslint'
  ],
  parserOptions: {
    parser: '@typescript-eslint/parser',
    sourceType: 'module', // allow the use of imports statements
    ecmaVersion: 2020
  },
  ignorePatterns: ['**/*.svg', '**/svgs/*.vue'],
  rules: {
    'no-console': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'no-debugger': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'import/prefer-default-export': 'off',
    // 'max-len': ['error', { code: 170, tabWidth: 2, ignoreComments: true, ignoreTrailingComments: true }],
    'lines-between-class-members': ['error', 'always', { exceptAfterSingleLine: true }],
    'comma-dangle': ['error', 'never'],
    'object-curly-newline': ['error', { ImportDeclaration: { multiline: true, minProperties: 10 }, ExportDeclaration: 'never' }],
    'no-bitwise': ['error', { allow: ['~', '<<', '|'] }],
    'import/no-cycle': 0,
    'no-param-reassign': [2, { props: false }],
    'arrow-body-style': ['error', 'as-needed'],
    'implicit-arrow-linebreak': 'off',
    'function-paren-newline': ['error', 'consistent'],
    'linebreak-style': ['error', 'unix']
  }
};
