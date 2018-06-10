using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.ComponentModel;

namespace AWArtis.Models
{
 //   [Table("Articus")]
    public class Articu :INotifyPropertyChanged
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id {
            get { return _id; }
            set {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _art_cod;
        [NotNull]
        public string Art_cod {
            get { return _art_cod; }
            set
            {
                this._art_cod = value;
                OnPropertyChanged(nameof(Art_cod));
            }
        }

        private string _art_des;
        [MaxLength(50)]
        public string Art_des
        {
            get { return _art_des; }
            set
            {
                this._art_des = value;
                OnPropertyChanged(nameof(Art_des));
            }
        }

        private double _art_preven1;
        public double Art_preven1
        {
            get { return _art_preven1; }
            set
            {
                this._art_preven1 = value;
                OnPropertyChanged(nameof(Art_preven1));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }

    }
}
