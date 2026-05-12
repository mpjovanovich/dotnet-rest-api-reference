using DotnetRestApiReference.Domain.Exceptions;
using DotnetRestApiReference.Domain.Models;
using DotnetRestApiReference.Domain.Interfaces.Repositories;
using Microsoft.Data.Sqlite;

namespace DotnetRestApiReference.Infrastructure.SQLite;

public class SQLiteBirdsRepository : IBirdsRepository
{
    private readonly SqliteConnection _connection;

    public SQLiteBirdsRepository(string connectionString)
    {
        _connection = new SqliteConnection(connectionString);
        _connection.Open();
    }

    /*
    PUBLIC API
    */
    public Bird Add(Bird bird)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public List<Bird> GetAll()
    {
        throw new NotImplementedException();
    }

    public Bird? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Bird Update(Bird bird)
    {
        throw new NotImplementedException();
    }
}
