# Dotnet Rest API Reference

## Overview

This is a reference project for a REST API built with .NET Minimal APIs. It is meant as a starting point for new projects and a best practices showcase.

There are several branches, each with a different level of complexity and features.

## Project Structure

The project is structured as follows:

- `Data`: Contains init sql scripts and/or migrations where applicable
- `DTOs/`: Contains the data transfer objects for the API. These are the contracts for requests and responses.
- `Endpoints/`: Contains the endpoints for the API. These are the routes, handlers, and controllers for the API.
- `Models/`: Contains the data models for the API.
- `Program.cs`: Contains the main entry point for the API.

The project additionally has a Postman configuration file in the `Postman/` directory.

## Branches

### no-dal

The most minimalistic branch. It does not use a database or DAL; it simply uses built-in data structures and methods to store and retrieve data.

### sqlite-ado

Uses SQLite with ADO.NET to store and retrieve data.

### sqlite-ef-core

Uses SQLite with EF Core to store and retrieve data.

### sqlite-dapper

Uses SQLite with Dapper to store and retrieve data.

### postgres-dapper

Uses Postgres with Dapper to store and retrieve data.
