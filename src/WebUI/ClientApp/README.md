# Smarthub

## Project setup

`yarn` or `yarn install`
Than (for safty)
`yarn postinstall` (this should be run automatically because "postinstall" is a yarn lifecycle, but it doesn't produce an output)  
For mor info about yarn 2 please visit their [site](https://yarnpkg.com/getting-started).

### Compiles and hot-reloads for development

`yarn dev` or `yarn run dev`

### Compiles and minifies for production

`yarn build` or `yarn run build`

### Lints and fixes files

These commands will look for and fix eslint and prettier issues.  
`yarn lint`
`yarn lint:fix`  
(`yarn prettier`
`yarn prettier:fix`)  
__Prettier__ is also a plugin for __eslint__, so there is no actual need to call these commands separately.

### Pre-commit

Before each commit a [husky](https://typicode.github.io/husky/#/) command is called, wich calls `yarn lint:fix`.  

If you use "GitKraken" the husky commands will not work properly. They work with VsCode, Jetbrains and terminals.

For the husky integration I followed [this](https://scottsauber.com/2021/06/01/using-husky-git-hooks-and-lint-staged-with-nested-folders/) and [this](https://moduscreate.com/blog/lint-style-typescript/).
 
### Customize configuration

See [vitejs Configuration Reference](https://vitejs.dev/).
