# Dotnet Rest API Reference

## Overview

This is a reference project for a REST API built with .NET Minimal APIs. It is meant to demonstrate the basic layering patterns and architecture for a REST API.

There are several branches, each with a different level of complexity and features.

## Layers

Data moves through the layers in the following order:

```
HTTP Request -> Endpoints -> Services -> Repositories -> (store)
```

_Note that earlier branches may not have all layers implemented. This is intentional to keep them focused and simple._

## Project Structure

### DotnetRestApiReference.Api

This is the composition root for the application. It is responsible for configuring the application and its dependencies.

This corresponds to the interface layer, handling the HTTP requests and responses.

### DotnetRestApiReference.Domain

This is the domain layer. It is responsible for the business logic and domain validation.

### DotnetRestApiReference.Infrastructure

This is the infrastructure layer. It is responsible for implementing data access and persistence logic.

## Branches

The following branches are planned:

### no-di

The most minimalistic branch. It does not use a database or repository layer; it uses static classes that have some knowledge of each other. This will be decoupled in future branches.

### pure-di

Splits into projects and indtroduces the necessary seams and interfaces to allow for constructor injection. Still uses an in-memory data store.

### di-container

Uses a DI container to manage dependencies instead of pure DI. Still uses an in-memory data store.

### sqlite-ado

Uses SQLite database with ADO.NET

### sqlite-ef-core

Uses SQLite database with EF Core

### sqlite-dapper

Uses SQLite database with Dapper

### postgres-dapper

Uses Postgres database with Dapper

### vertical-slice

Potential vertical slice architecture implementation. Most likely build on top of the sqlite-dapper branch.
