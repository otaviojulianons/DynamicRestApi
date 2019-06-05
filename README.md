[![Build Status](https://travis-ci.com/otaviojulianons/DynamicRestApi.svg?branch=master)](https://travis-ci.com/otaviojulianons/DynamicRestApi) [![Maintainability](https://api.codeclimate.com/v1/badges/99041474b10fc2e37f9d/maintainability)](https://codeclimate.com/github/otaviojulianons/DynamicRestApi/maintainability)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=DynamicRestApi&metric=code_smells)](https://sonarcloud.io/project/issues?id=DynamicRestApi&resolved=false&types=CODE_SMELL)
[![Sonarcloud Status](https://sonarcloud.io/api/project_badges/measure?project=DynamicRestApi&metric=alert_status)](https://sonarcloud.io/dashboard?id=DynamicRestApi) 
[![SonarCloud Coverage](https://sonarcloud.io/api/project_badges/measure?project=DynamicRestApi&metric=coverage)](https://sonarcloud.io/component_measures/metric/coverage/list?id=DynamicRestApi)
[![SonarCloud Bugs](https://sonarcloud.io/api/project_badges/measure?project=DynamicRestApi&metric=bugs)](https://sonarcloud.io/component_measures/metric/reliability_rating/list?id=DynamicRestApi)
[![SonarCloud Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=DynamicRestApi&metric=vulnerabilities)](https://sonarcloud.io/component_measures/metric/security_rating/list?id=DynamicRestApi)


# DyRA - Dynamic Rest Api
Generate Rest API from Dynamic Entities

## Getting Started

From the mapping of relational entities DyRA is able to generate dynamic endpoints and [OpenAPI Specification](https://www.openapis.org).

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites
- [SQL Server 2017](https://www.microsoft.com/en-us/sql-server/sql-server-2017) database installed


### Installing

Configure database connection in appsettings.json file

```json
  "Database": {
    "ConnectionString": "(Your string connection)"
  }
```

Run the project and check its documentation at http://localhost:5000/swagger


## Built With

* [Asp.Net Core](https://docs.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-2.0) - C-Sharp Web API 
* [SQL Server 2017](https://www.microsoft.com/en-us/sql-server/sql-server-2017) - Database
* [EntityFramework Core](https://docs.microsoft.com/pt-br/ef/core/) - Relational Object Mapper 
* [Mustache ](http://mustache.github.io/) - Generate code templates
* [Swagger ](https://swagger.io/) - API documentation

## Authors

* **Otávio Juliano Nielsen Silva** - *Initial work* - [otaviojulianons](https://github.com/otaviojulianons)

See also the list of [contributors](https://github.com/your/project/contributors) who participated in this project.

## License

This project is licensed under the MIT License
