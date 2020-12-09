/* eslint-disable prettier/prettier */
module.exports = {
  client: {
    addTypename: true,
    service: {
      name: 'SmartHub',
      // URL to the GraphQL API
      url: 'https://localhost:5001/graphql',
      skipSSLValidation: true
    },
    // Files processed by the extension
    includes: [
      'src/**/*.vue',
      'src/**/*.js',
      'src/**/*.ts'
    ]
  }
}
