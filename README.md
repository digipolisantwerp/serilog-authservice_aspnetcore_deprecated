# Serilog AuthService Library

DEPRECATED - moved to internal Bitbucket

Serilog enricher for Digipolis AuthService.

## Table of Contents

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->


- [AuthServiceEnricher](#AuthServiceenricher)
- [Installation](#installation)
- [Usage](#usage)
- [Enricher](#enricher)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## AuthServiceEnricher

This library contains an enricher for Serilog that adds the IAuthService properties to the LogEvent.
You can find more info about the IAuthService here : [https://github.com/digipolisantwerp/auth_aspnetcore](https://github.com/digipolisantwerp/auth_aspnetcore).

## Installation

This package is hosted on Myget on the following feed : https://www.myget.org/F/digipolisantwerp/api/v3/index.json.
To add it to a project, you add the package to the csproj project file:

```xml
  <ItemGroup>
    <PackageReference Include="Digipolis.Serilog.AuthService" Version="5.0.0" />
  </ItemGroup>
``` 

In Visual Studio you can also use the NuGet Package Manager to do this.

## Usage

The AuthServiceEnricher has a dependency on the IAuthService of the **Digipolis.Auth** package, so make sure the needed services are 
[registered](https://github.com/digipolisantwerp/auth_aspnetcore#startup). Then register the AuthServiceEnricher in the .NET core DI container. This can be done 
in combination with the Serilog Extensions library found here : [https://github.com/digipolisantwerp/serilog_aspnetcore](https://github.com/digipolisantwerp/serilog_aspnetcore) 
by calling the **AddAuthServiceEnricher()** method in the Configure method of the Startup class :

```csharp
services.AddSerilogExtensions(options => {
    options.AddAuthServiceEnricher();
});
```  

## Enricher

The enricher adds the following fields to the Serilog LogEvent :

- MessageUser : the user that under whose context the messages is logged.
- MessageUserIsAuthenticated : indicates if the user is authenticed.

## Contributing

Pull requests are always welcome, however keep the following things in mind:

- New features (both breaking and non-breaking) should always be discussed with the [repo's owner](#support). If possible, please open an issue first to discuss what you would like to change.
- Fork this repo and issue your fix or new feature via a pull request.
- Please make sure to update tests as appropriate. Also check possible linting errors and update the CHANGELOG if applicable.

## Support

Peter Brion (<peter.brion@digipolis.be>)
