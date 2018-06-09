using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AWArtis.Helpers;
using AWArtis.Models;
using AWArtis.Services.Sqlite;
using SQLite.Net;
using Xamarin.Forms;

namespace AWArtis.Services.Sqlite
{
    public class SqliteService : ISqliteService
    {
        private static readonly AsyncLock Mutex = new AsyncLock();
        private SQLiteAsyncConnection _sqlCon;

        public SqliteService()
        {
            var databasePath = DependencyService.Get<IPathService>().GetDatabasePath();
            _sqlCon = new SQLiteAsyncConnection(databasePath);

            CreateDatabaseAsync();
        }

        public async void CreateDatabaseAsync()
        {
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                await _sqlCon.CreateTableAsync<Articu>().ConfigureAwait(false);
            }
        }

        public async Task<IList<Articu>> GetAll()
        {
            var items = new List<Articu>();
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                items = await _sqlCon.Table<Articu>().ToListAsync().ConfigureAwait(false);
            }

            return items;
        }

        public async Task Insert(Articu item)
        {
            using (await Mutex.LockAsync().ConfigureAwait(false))
            {
                var existingTodoItem = await _sqlCon.Table<Articu>()
                        .Where(x => x.Id == item.Id)
                        .FirstOrDefaultAsync();

                if (existingTodoItem == null)
                {
                    await _sqlCon.InsertAsync(item).ConfigureAwait(false);
                }
                else
                {
                    item.Id = existingTodoItem.Id;
                    await _sqlCon.UpdateAsync(item).ConfigureAwait(false);
                }
            }
        }

        public async Task Remove(Articu item)
        {
            await _sqlCon.DeleteAsync(item);
        }
    }
}
