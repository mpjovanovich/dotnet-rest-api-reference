# Dotnet Rest API Reference

## Overview

This is a reference project for a REST API built with .NET Minimal APIs. It is meant as a starting point for new projects and a best practices showcase.

There are several branches, each with a different level of complexity and features.

## Layers

Data moves through the layers in the following order:

```
HTTP Request -> Endpoints -> Handlers -> Services
```

## Project Structure

The project is structured as follows:

### `Data/`

Contains init sql scripts and/or migrations where applicable

### `DTOs/`

Contains the data transfer objects for the API. These are the contracts for requests and responses.

Does not change between branches.

### `Endpoints/`

Contains route registration and HTTP mapping logic.

Does not change between branches.

### `Handlers/`

Contains input validation and delegates to the service layer.

### `Models/`

Contains the data models for the API.

Does not change between branches.

### `Services/`

Contains the business/domain logic, domain validation, and orchestrates data access.

We have decided not to implement a separate repository layer for this project. There is no need to abstract the data store, so the complexity is not worth it.

### `Program.cs`

Contains the main entry point for the API.

The project additionally has a Postman configuration file in the `Postman/` directory.

## Branches

The following branches are planned:

### no-dal

The most minimalistic branch. It does not use a database or DAL; it simply uses built-in data structures and methods to store and retrieve data.

### sqlite-ado

Uses SQLite with ADO.NET

### sqlite-ef-core

Uses SQLite with EF Core

### sqlite-dapper

Uses SQLite with Dapper

### postgres-dapper

Uses Postgres with Dapper

### vertical-slice

Potential vertical slice architecture implementation. Most likely build on top of the sqlite-dapper branch.
