# Alpaki

### Backend
[![Build Status](https://travis-ci.org/arborQ/Alpaki.svg?branch=master)](https://travis-ci.org/arborQ/Alpaki)
![.github/workflows/ci.yml](https://github.com/arborQ/Alpaki/workflows/.github/workflows/ci.yml/badge.svg)
* .net core 3.1
* EntityFramework
* GraphQL
* Swagger
* MediatR
* FluentValidator
* Seq
* Docker

### Run backend

```sh
$ docker-compose up --build
```

See Swagger:
http://localhost:5000/swagger/index.html

To authorize as admin, access Seq logs

http://localhost:5341

and see for admin token you can use

```sh
$ [ADMIN]: admin user was created with token: [TOKEN]
```

