[![Build Status](https://travis-ci.com/otaviojulianons/DynamicRestApi.svg?branch=master)](https://travis-ci.com/otaviojulianons/DynamicRestApi)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=otaviojulianons_DynamicRestApi&metric=code_smells)](https://sonarcloud.io/project/issues?id=otaviojulianons_DynamicRestApi&resolved=false&types=CODE_SMELL)
[![Duplications](https://sonarcloud.io/api/project_badges/measure?project=otaviojulianons_DynamicRestApi&metric=duplicated_lines_density)](https://sonarcloud.io/component_measures?id=otaviojulianons_DynamicRestApi&metric=Duplications)
[![SonarCloud Coverage](https://sonarcloud.io/api/project_badges/measure?project=otaviojulianons_DynamicRestApi&metric=coverage)](https://sonarcloud.io/component_measures/metric/coverage/list?id=otaviojulianons_DynamicRestApi)
[![SonarCloud Bugs](https://sonarcloud.io/api/project_badges/measure?project=otaviojulianons_DynamicRestApi&metric=bugs)](https://sonarcloud.io/component_measures/metric/reliability_rating/list?id=otaviojulianons_DynamicRestApi)
[![Code Lines](https://sonarcloud.io/api/project_badges/measure?project=otaviojulianons_DynamicRestApi&metric=ncloc)](https://sonarcloud.io/code?id=otaviojulianons_DynamicRestApi)


# DyRA - Dynamic Rest Api
Generate Rest API from Dynamic Entities

## Getting Started

Dyra is an API capable of generating a dynamic resource list. During application initialization, the API infrastructure (_Controllers, Entities and Models_) is compiled at runtime through an entities register.

## Features

* Generate REST API (GET \ POST \ UPDATE \ DELETE)
* Generate Dynamic Documentation by Swagger
* Generate GraphQL Integration
* Mapping Complex Objects
* Pagination
* Dynamic filter with LINQ

## Next Features

* SignalR Integration
* gRPC Integration

## Built With

* [ASP.NET Core](https://docs.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-2.0)
* [MongoDB](https://www.mongodb.com/)
* [Mustache](http://mustache.github.io/)
* [Swagger](https://swagger.io/)
* [System.Linq.Dynamic.Core](https://github.com/StefH/System.Linq.Dynamic.Core)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://github.com/AutoMapper/AutoMapper)
* [FluentValidation](https://github.com/FluentValidation/FluentValidation)
* [GraphQL for .NET](https://github.com/graphql-dotnet/graphql-dotnet)

## Author

* **Otávio Juliano Nielsen Silva** - *Initial work* - [otaviojulianons](https://github.com/otaviojulianons)

## License

This project is licensed under the MIT License
