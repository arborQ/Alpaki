const history = require('connect-history-api-fallback');
const express = require('express');
const app = express()
const port = 8080;

app.use(history({
  verbose: true
}));

app.use(express.static('.'));
app.listen(port, function () {
  console.log(`Example app listening on port http://localhost:${port}!`)
});