require('dotenv').config();
const history = require('connect-history-api-fallback');
const express = require('express');
const httpProxy = require('http-proxy');
const app = express()
const port = 8080;
const elasticSearchUrl = process.env.ELASTIC_SEARCH_URL;
var apiProxy = httpProxy.createProxyServer({
  changeOrigin: true
});

var azureFunctionsProxy = httpProxy.createProxyServer({
  changeOrigin: true
});

apiProxy.on('proxyReq', function (proxyReq) {
  proxyReq.setHeader('api-key', process.env.API_KEY);
  proxyReq.setHeader('content-type', 'application/json; odata.metadata=minimal');
});

app.post("/api/search", function (req, res) {
  req.url = `/indexes/${req.query.index}/docs/search?api-version=${process.env.API_VERSION}`;
  apiProxy.web(req, res, { target: elasticSearchUrl });
});


app.post("/api/ValidateUser", function (req, res) {
  azureFunctionsProxy.web(req, res, { target: "https://alpaki-party.azurewebsites.net/api/ValidateUser" });
});

app.use(express.json());
app.use(express.urlencoded({extended: true}));

app.use(history({
  verbose: true
}));

app.use(express.static('.'));
app.listen(port, function () {
  console.log(`Example app listening on port http://localhost:${port}!`)
});