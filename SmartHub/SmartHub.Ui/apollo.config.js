/* eslint-disable prettier/prettier */
module.exports = {
  client: {
    service: {
      name: 'SmartHub',
      // URL to the GraphQL API
      url: 'https://localhost:5001/graphql'
    },
    // Files processed by the extension
    includes: [
      'src/**/*.vue',
      'src/**/*.js'
    ]
  }
}
