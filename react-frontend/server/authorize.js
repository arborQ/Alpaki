const jwt = require('jsonwebtoken');
const key = process.env.JWT_PRIVATE_KEY;

const users = [
    { login: 'admin', password: 'test123!', mode: 3 },
    { login: 'kasia', password: '29032021', mode: 2 },
];

exports.AuthorizeUser = function (req, res) {
    const user = users.find(u => u.login === req.body.login && u.password === req.body.password);

    if (!!user) {
        var token = jwt.sign({ login: user.login, mode: user.mode }, key, { algorithm: 'RS256' });
        res.send({ token });
    } else {
        res.statusMessage = "Current password does not match";
        res.status(400).end();
    }

}
