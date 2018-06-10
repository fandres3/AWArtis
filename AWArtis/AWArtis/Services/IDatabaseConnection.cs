using System;
using System.Collections.Generic;
using System.Text;

namespace AWArtis.Services
{
    public interface IDatabaseConnection
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
