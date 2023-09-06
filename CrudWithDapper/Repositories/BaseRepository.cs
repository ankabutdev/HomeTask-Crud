using CrudWithDapper.Database;
using Npgsql;

namespace CrudWithDapper.Repositories;

public class BaseRepository
{
    protected readonly NpgsqlConnection _connection;

    public BaseRepository()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        this._connection = new NpgsqlConnection(DbConstant.DB_CONNECTION_STRING);
    }
}
