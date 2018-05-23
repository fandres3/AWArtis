using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AWArtis.Models
{
    public class Articu
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Art_cod { get; set; }
        public string Art_des { get; set; }
        public double Art_preven1 { get; set; }
    }
}
