using System;

using System.Numerics;
using System.ComponentModel;


namespace Lab1
{
    abstract class V3Data
    {

        public string meas_ident;

        public DateTime d_time;

        

        public event PropertyChangedEventHandler PropertyChanged;


        

        public V3Data(string id, DateTime t)
        {
            meas_ident = id;
            d_time = t;
        }

        public string measure
        {
            get
            {
                return meas_ident;
            }
            set
            {
                meas_ident = value;
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
                PropertyChanged(this, new PropertyChangedEventArgs("date was changed"));
            }
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