using DotnetRestApiReference.Domain.Exceptions;
using DotnetRestApiReference.Domain.Models;
using DotnetRestApiReference.Domain.Interfaces.Repositories;
using Microsoft.Data.Sqlite;

namespace DotnetRestApiReference.Infrastructure.SQLite;

public class SQLiteRegionsRepository : IRegionsRepository
{
    private readonly SqliteConnection _connection;

    public SQLiteRegionsRepository(string connectionString)
    {
        _connection = new SqliteConnection(connectionString);
        _connection.Open();
    }

    /*
    PUBLIC API
    */
    public Region Add(Region region)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public List<Region> GetAll()
    {
        throw new NotImplementedException();
    }

    public Region? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Region Update(Region region)
    {
        throw new NotImplementedException();
    }
}
