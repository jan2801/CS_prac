using System;
using System.Collections;
using System.Numerics;
using System.Collections.Generic;


namespace Lab1
{
    class V3DataCollection: V3Data, IEnumerable<DataItem>
    {

        
        public List<DataItem> lst_d { get; set; }

        public V3DataCollection(string st, DateTime d_t) : base(st, d_t)
        {
            measure = st;
            date = d_t;
            lst_d = new List<DataItem>();
        }
        
        public void InitRandom(int nItems, float xmax, float ymax, double minValue, double maxValue)
        {
            double elec;
            float x_coor;
            float y_coor;

            for (int i = 0; i < nItems; i++)
            {
                Random rand = new Random();
                Vector2 new_coor;
                DataItem el;
                x_coor = (float) rand.NextDouble() * xmax;
                y_coor = (float) rand.NextDouble() * ymax;
                new_coor = new Vector2(x_coor, y_coor);

                elec = (double) rand.NextDouble() * (maxValue - minValue) + minValue;
                el = new DataItem(new_coor, elec);
                lst_d.Add(el);
            }
        }

        public override Vector2[] Nearest(Vector2 v)
        {
            List<Vector2> near = new List<Vector2>();
            float ebs = 0.000000001F;
            float minim;
            minim = Vector2.Distance(lst_d[0].coor, v);
        
            
            foreach (DataItem item in lst_d)
            {
                if ((Math.Abs(Vector2.Distance(item.coor, v) - minim)) < ebs)
                {
                     
                        near.Add(item.coor);
              
                }
                else if (Vector2.Distance(item.coor, v) < minim)
                {
                    near.Clear();
                    minim = Vector2.Distance(item.coor, v);
                    near.Add(item.coor);

                }
            }
     
            
            Vector2[] shortest = near.ToArray();

            

            return shortest;
        }

        public override string ToString()
        {
            string s;
            s = "V3DataCollection" + " " + measure + " " + date.ToString() + " " + lst_d.Count.ToString() + "\n";
            return s;
        }
        public override string ToLongString()
        {
            string s;
            s = "V3DataCollection" + " " + measure + " " + date.ToString() + " " + lst_d.Count.ToString() + "\n";
            foreach (DataItem item in lst_d)
            {
                s += item.ToString() + "\n";
            }
            return s;
        }


        public override string ToLongString(string format)
        {
            string s;
            s = "V3DataCollection" + " " + measure + " " + date.ToString(format) + " " + lst_d.Count.ToString(format) + "\n";
            foreach (DataItem item in lst_d)
            {
                s += item.ToString(format) + "\n";
            }
            return s;
        }


        public IEnumerator<DataItem> GetEnumerator()
        {
            return lst_d.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            
            return lst_d.GetEnumerator();
        }


    }
}