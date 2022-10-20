using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace MobileAssigenment2
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection(string database);
    }
}
