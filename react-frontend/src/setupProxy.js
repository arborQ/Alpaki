const { createProxyMiddleware } = require('http-proxy-middleware');
require('dotenv').config();

console.log(process.env.PROXY);

module.exports = function(app) {
  app.use(
    '/api',
    createProxyMiddleware({
      target: process.env.PROXY,
      changeOrigin: true,
    })
  );
};