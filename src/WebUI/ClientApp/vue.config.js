module.exports = {
  devServer: {
    proxy: {
      '^/api': {
        target: 'https://localhost:5001'
      },
      '^/graphql': {
        target: 'https://localhost:5001'
      }
    }
  },
  outputDir: '../wwwroot'
};
