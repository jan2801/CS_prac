using System;

using System.Numerics;


namespace Lab1
{
    abstract class V3Data
    {

        public string meas_ident{ get; set; }

        public DateTime d_time { get; set; }

        public V3Data(string id, DateTime t)
        {
            meas_ident = id;
            d_time = t;
        }

        public abstract string ToLongString();
        public abstract Vector2[] Nearest(Vector2 v);

        public override string ToString()
        {
            string st_1 = "V3Data";
            return (st_1 + " " + meas_ident + " " + d_time.ToString());
        }
        



    }

}