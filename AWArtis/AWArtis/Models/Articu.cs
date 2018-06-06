using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;

namespace AWArtis.Models
{
    [Table("Articu")]
    public class Articu 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Art_cod { get; set; }
        public string Art_des { get; set; }
        public double Art_preven1 { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }

    }
}
