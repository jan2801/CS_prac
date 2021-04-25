using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ClassLibrary;


namespace WpfApp
{
    class DataItemBinding: IDataErrorInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private float x, y;
        private double current_field;
        V3DataCollection DataCollection { get; set; }
        public DataItemBinding(ref V3DataCollection col)
        {
            DataCollection = col;
        }
        public string Error { get { return "error"; } }

        public float x_xoord
        {
            get { return x; }
            set {
                x = value;
                PropertyChangedDetected("x");

            }
        }

        public float y_coord
        {
            get { return y; }
            set {
                y = value;
                PropertyChangedDetected("y");
            }
        }

        public double field
        {
            get { return current_field; }
            set {
                current_field = value;
                PropertyChangedDetected("field");
            }
        }



        public string this[string columnName]
        {
            get
            {

            }
        }

        public void PropertyChangedDetected(string s)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs($"{s} property was changed"));
        }


        public void Add(System.Numerics.Vector2 coordinates, double f)
        {
            DataCollection.lst_d.Add(new DataItem(coordinates, f));
        }
    }
}
