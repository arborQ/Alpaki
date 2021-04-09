const { createProxyMiddleware } = require('http-proxy-middleware');
var httpProxy = require('http-proxy');

require('dotenv').config();
const elasticSearchUrl = process.env.ELASTIC_SEARCH_URL;

module.exports = function (app) {
  var azureSearchProxy = httpProxy.createProxyServer({
    changeOrigin: true
  });

  var azureFunctionsProxy = httpProxy.createProxyServer({
    changeOrigin: true
  });

  azureSearchProxy.on('proxyReq', function (proxyReq) {
    proxyReq.setHeader('api-key', process.env.API_KEY);
    proxyReq.setHeader('content-type', 'application/json; odata.metadata=minimal');
  });

  app.post("/api/search", function (req, res) {
    req.url = `/indexes/${req.query.index}/docs/search?api-version=${process.env.API_VERSION}`;
    azureSearchProxy.web(req, res, { target: elasticSearchUrl });
  });

  app.use("/api/*", function (req, res) {
    console.log(req.url);
    azureFunctionsProxy.web(req, res, { target: "https://alpaki-party.azurewebsites.net/api/ValidateUser" });
  });

  app.use(
    '/api',
    createProxyMiddleware({
      target: process.env.PROXY,
      changeOrigin: true,
    })
  );
};