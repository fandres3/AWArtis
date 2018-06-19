using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using System.Linq;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using AWArtis.Models;
using System.IO;

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
                SaveAllArticus();
             
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
            this.Articus.
             Add(new Articu
             {
                 Art_cod = "A2",
                 Art_des = "de22222scripcion a2",
                 Art_preven1 = 22.02
             });
        }
        // Use LINQ to query and filter data
        //public IEnumerable<Articu> GetFilteredArticus(string codigo)
        //{
        //    // Use locks to avoid database collitions
        //    lock (collisionLock)
        //    {
        //        var query = from art in database.Table<Articu>()
        //                    where art.Art_cod == codigo
        //                    select art;
        //        return query.AsEnumerable();
        //    }
        //}
        // Use SQL queries against data
        public IEnumerable<Articu> GetFilteredArticus(string codigo,string descripcion)
        {
            lock (collisionLock)
            {
                if (codigo != null)
                {
                    return database.
   Query<Articu>
   ("SELECT * FROM Articu where Art_cod LIKE '" + codigo + "%' ORDER BY Art_cod").AsEnumerable();
                }

                if (descripcion != null)
                {
                    return database.
   Query<Articu>
   ("SELECT * FROM Articu where Art_des LIKE '" + descripcion + "%' ORDER BY Art_des").AsEnumerable();
                }
                return null;

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
        public int DeleteArticu(Articu articuInstance)
        {
            var id = articuInstance.Id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Articu>(id);
                }
            }
            this.Articus.Remove(articuInstance);
            return id;
        }
        public void DeleteAllArticus()
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

