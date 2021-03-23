const express = require('express')
const app = express()
const port = 443;

app.use(express.static('./'));

app.listen(port, () => {
  console.log(`App listening at https://localhost:${port}`)
});
