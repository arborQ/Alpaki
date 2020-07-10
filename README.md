# Alpaki

### Backend
[![Build Status](https://travis-ci.org/arborQ/Alpaki.svg?branch=master)](https://travis-ci.org/arborQ/Alpaki)

* .net core 3.1
* EntityFramework
* GraphQL
* Swagger
* MediatR
* FluentValidator

### Run backend

```sh
$ dotnet ef database update -p .\Alpaki.Database\ -s .\Alpaki.WebApi\
$ dotnet run -p .\backend\Alpaki.WebApi
```

See Swagger:
https://localhost:5001/swagger/index.html

Play with GraphQL:
https://localhost:5001/ui/playground