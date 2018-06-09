using System;
using System.Collections.Generic;
using System.Text;
using SQLite.Net;
using SQLite.Net.Attributes;

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
    //https://javiersuarezruiz.wordpress.com/2016/06/15/xamarin-utilizando-sqlite/

}
