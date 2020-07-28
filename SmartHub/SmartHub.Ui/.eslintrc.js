module.exports = {
  root: true,
  env: {
    node: true
  },
  extends: [
    'plugin:prettier/recommended',
    "plugin:vue/vue3-essential",
    '@vue/airbnb',
    // "eslint:recommended",
    "@vue/typescript/recommended",
    "@vue/prettier/@typescript-eslint"
  ],
  parserOptions: {
    parser: "@typescript-eslint/parser", // the typescript-parser for eslint, instead of tslint
    sourceType: "module", // allow the use of imports statements
    ecmaVersion: 2020
  },
  ignorePatterns: ["**/*.svg", "**/svgs/*.vue"],
  rules: {
    'no-console': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'no-debugger': process.env.NODE_ENV === 'production' ? 'warn' : 'off',
    'import/prefer-default-export': 'off',
    'max-len': [2, 130, 2],
    'lines-between-class-members': ['error', 'always', { exceptAfterSingleLine: true }],
    'comma-dangle': ['error', 'never'],
    'object-curly-newline': [
      'error',
      { ImportDeclaration: { multiline: true, minProperties: 6 }, ExportDeclaration: 'never' }
    ],
    'no-bitwise': ['error', { allow: ['~', '<<', '|'] }],
    'import/no-cycle': 0,
    'no-param-reassign': [2, { props: false }]
  }
};
