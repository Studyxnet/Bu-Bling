using System;

using SQLite.Net;

namespace bubling
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}

