using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ClassLibrary;
using System.Numerics;

namespace WpfApp
{
    class DataItemBinding: IDataErrorInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private float x, y;
        private double current_field;
        V3DataCollection DataCollection { get; set; }
        public DataItemBinding(V3DataCollection col)
        {
            DataCollection = col;
        }
        public string Error { get { return "error"; } }

        public float x_coord
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



        public string this[string s]
        {
            get
            {
                string msg ="";
                switch (s)
                { 
                    
                    case "x":
                        goto case "y";
                    case "y":
                        if (DataCollection.lst_d.Contains(new DataItem(new Vector2(x, y), field)))
                        {
                            msg = "This element is already in collection";
                        }
                        goto case "field";

                    case "field":
                        if (field < 0)
                        {
                            msg = "Value os the field can't be less then 0";
                        }
                        break;
                }
                return msg;
                
            }
        }

        public void PropertyChangedDetected(string s)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs($"{s} property was changed"));
        }


        public void Add()
        {
            Vector2 cooordinates = new Vector2(x_coord, y_coord);
            DataCollection.lst_d.Add(new DataItem(cooordinates, field));
        }
    }
}
