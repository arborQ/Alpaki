require('dotenv').config();
const express = require('express');
var httpProxy = require('http-proxy');
var apiProxy = httpProxy.createProxyServer({
    changeOrigin: true
});

const app = express()
const port = 8080;
const elasticSearchUrl = process.env.ELASTIC_SEARCH_URL;

apiProxy.on('proxyReq', function (proxyReq) {
    proxyReq.setHeader('api-key', process.env.API_KEY);
    proxyReq.setHeader('content-type', 'application/json; odata.metadata=minimal');
});

app.post("/api/search", function (req, res) {
    req.url = `/indexes/${req.query.index}/docs/search?api-version=${process.env.API_VERSION}`;
    apiProxy.web(req, res, { target: elasticSearchUrl });
});

app.listen(port, function () {
    console.log(`Example app listening on port http://localhost:${port}! and forward to ${elasticSearchUrl}`)
});