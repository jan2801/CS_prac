using System;

using System.Numerics;
using System.ComponentModel;


namespace Lab1
{
    abstract class V3Data: INotifyPropertyChanged
    {

        private string meas_ident;

        private DateTime d_time;

        

        public event PropertyChangedEventHandler PropertyChanged;


        

        
        public string measure
        {
            get
            {
                return meas_ident;
            }
            set
            {
                meas_ident = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("measure was changed"));
            }
        }

        public DateTime date
        {
            get
            {
                return d_time;
            }
            set
            {
                d_time = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("date was changed"));
            }
        }


        public V3Data(string id, DateTime t)
        {
            measure = id;
            date= t;
        }


        public abstract string ToLongString();

        public abstract string ToLongString(string format);
       
        public abstract Vector2[] Nearest(Vector2 v);

        public override string ToString()
        {
            string st_1 = "V3Data";
            return (st_1 + " " + meas_ident + " " + d_time.ToString());
        }
        



    }

}