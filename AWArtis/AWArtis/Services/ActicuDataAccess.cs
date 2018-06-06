using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using AWArtis.Models;

namespace AWArtis.Services
{
    public class ArticusDataAccess
    {

        private SQLiteConnection database;
        private static object collisionLock = new object();
        public ObservableCollection<Articu> Articus { get; set; }
        public ArticusDataAccess()
        {
            database =
              DependencyService.Get<IDatabaseConnection>().
              DbConnection();
            database.CreateTable<Articu>();
            this.Articus =
              new ObservableCollection<Articu>(database.Table<Articu>());
            // If the table is empty, initialize the collection
            if (!database.Table<Articu>().Any())
            {
                AddNewCustomer();
            }
        }
        public void AddNewCustomer()
        {
            this.Articus.
              Add(new Articu
              {
                  Art_cod="A1",
                  Art_des="descripcion a1",
                  Art_preven1 = 33.33
              });
        }
        // Use LINQ to query and filter data
        public IEnumerable<Articu> GetFilteredCustomers(string codigo)
        {
            // Use locks to avoid database collitions
            lock (collisionLock)
            {
                var query = from cust in database.Table<Articu>()
                            where cust.Art_cod == codigo
                            select cust;
                return query.AsEnumerable();
            }
        }
        // Use SQL queries against data
        public IEnumerable<Articu> GetFilteredArticus()
        {
            lock (collisionLock)
            {
                return database.
                  Query<Articu>
                  ("SELECT * FROM Articu WHERE art_cod = 'A1'").AsEnumerable();
            }
        }
        public Articu GetArticu(string codigo)
        {
            lock (collisionLock)
            {
                return database.Table<Articu>().
                  FirstOrDefault(articu => articu.Art_cod == codigo);
            }
        }
        public int SaveArticu(Articu articuInstance)
        {
            lock (collisionLock)
            {
                if (articuInstance.Id != 0)
                {
                    database.Update(articuInstance);
                    return articuInstance.Id;
                }
                else
                {
                    database.Insert(articuInstance);
                    return articuInstance.Id;
                }
            }
        }
        public void SaveAllArticus()
        {
            lock (collisionLock)
            {
                foreach (var articuInstance in this.Articus)
                {
                    if (articuInstance.Id != 0)
                    {
                        database.Update(articuInstance);
                    }
                    else
                    {
                        database.Insert(articuInstance);
                    }
                }
            }
        }
        public int DeleteCustomer(Articu customerInstance)
        {
            var id = customerInstance.Id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Articu>(id);
                }
            }
            this.Articus.Remove(customerInstance);
            return id;
        }
        public void DeleteAllCustomers()
        {
            lock (collisionLock)
            {
                database.DropTable<Articu>();
                database.CreateTable<Articu>();
            }
            this.Articus = null;
            this.Articus = new ObservableCollection<Articu>
              (database.Table<Articu>());
        }
    }
}

