# Dotnet Rest API Reference

## Overview

This is a reference project for a REST API built with .NET Minimal APIs. It is meant to demonstrate the basic layering patterns and architecture for a REST API.

There are several branches, each with a different level of complexity and features.

## Layers

Data moves through the layers in the following order:

```
HTTP Request -> Endpoints -> Services
```

## Project Structure

The project is structured as follows:

### `Data/`

Contains init sql scripts and/or migrations where applicable

### `DTOs/`

Contains the data transfer objects for the API. These are the contracts for requests and responses.

Does not change between branches.

### `Endpoints/`

- Contains route registration and HTTP mapping logic.
- Contains input validation.
- Delegates to the service layer.
- Does not change between branches.

We have decided not to move handlers into their own classes for this project in favor of simplicity, readability, and locality of code.

### `Models/`

- Contains the data models for the API.
- Does not change between branches.

### `Services/`

- Contains the business/domain logic, domain validation, and orchestrates data access.

We have decided not to implement a separate repository layer for this project. There is no need to abstract the data store, so the additional complexity is not worth it.

### `Program.cs`

Contains the main entry point for the API.

The project additionally has a Postman configuration file in the `Postman/` directory.

## Branches

The following branches are planned:

### no-dal

The most minimalistic branch. It does not use a database or service layer; it simply uses built-in data structures and methods to store and retrieve data.

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
