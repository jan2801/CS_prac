using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using ClassLibrary;
using System.Numerics;

namespace WpfApp
{
    class DataItemBinding : IDataErrorInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private float x, y;
        private double current_field;
        public V3DataCollection DataCollection;
        
        public string Error { get { return "error"; } }

        public float x_coord
        {
            get { return x; }
            set
            {
                x = value;
                PropertyChangedDetected("x_coord");

                

            }
        }

        public float y_coord
        {
            get { return y; }
            set
            {
                y = value;
                PropertyChangedDetected("y_coord");
                
            }
        }

        public double field
        {
            get { return current_field; }
            set
            {
                current_field = value;
                PropertyChangedDetected("field");
            }
        }



        public string this[string s]
        {

            get
            {
                string msg = "";
                if (DataCollection != null)
                {


                    switch (s)
                    {

                        case "x_coord":
                            foreach (var el in DataCollection.lst_d)
                            {
                                Console.WriteLine($"{el.coor.X}");
                                Console.WriteLine($" {el.coor.Y}");
                                if ((el.coor.X == x_coord) && (el.coor.Y == y_coord))
                                {
                                    msg = "This element is already in collection";
                                    break;
                                }
                            }
                            break;
                        case "y_coord":

                            
                            foreach (var el in DataCollection.lst_d)
                            {
                                Console.WriteLine($"{el.coor.X}");
                                Console.WriteLine($" {el.coor.Y}");
                                if (el.coor.X == x_coord && el.coor.Y == y_coord)
                                {
                                    msg = "This element is already in collection";
                                    break;
                                }
                            }
                            break;

                       
                        case "field":
                            if (field < 0)
                            {
                                msg = "Value of the field can't be less then 0";
                            }
                            break;
                    }
                }
                return msg;

            }
        }

        public DataItemBinding(ref V3DataCollection col)
        {
            DataCollection = col;

        }
        public void PropertyChangedDetected(string s)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(s));
        }


        public void Add()
        {
            Vector2 cooordinates = new Vector2(x_coord, y_coord);
            DataCollection.Add(new DataItem(cooordinates, field));
            PropertyChangedDetected("x_coord");
            PropertyChangedDetected("y_coord");
            PropertyChangedDetected("field");

        }
    }
}
