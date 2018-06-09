using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AWArtis.Models;
using AWArtis.Services.Sqlite;

namespace AWArtis.Services.Sqlite
{
    public interface ISqliteService
    {
        Task<IList<Articu>> GetAll();
        Task Insert(Articu item);
        Task Remove(Articu item);
    }
}
