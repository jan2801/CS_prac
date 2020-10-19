using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Lab1
{
    class V3MainCollection: IEnumerable<V3Data>
    {
        List<V3Data> v3 = new List<V3Data>();

        public int Count
        {
            get { return v3.Count; }
        }

        public IEnumerator<V3Data> GetEnumerator()
        {
            return v3.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(V3Data m)
        {
            v3.Add(m);
        }


        public bool Remove(string id, DateTime date)
        {
            bool k = false;
            foreach (V3Data el in v3.ToList())
            {
                if (el.meas_ident == id && el.d_time == date)
                {
                    v3.Remove(el);
                    k = true;
                }
            }
            return k;
        }

        public override string ToString()
        {
            string st = "";
            foreach (V3Data el in v3.ToList())
            {
                st += el.ToString();
            }
            return st;
        }


        public void AddDefaults()
        {
            string s = "first one ";
            string s1 = "Hello my name is Anton ";
            string s2 = "Good morning ";
            float st_x = 1.5F;
            int num_x = 10;
            float st_y = 1.5F;
            int num_y = 10;
            Grid1D x1 = new Grid1D (st_x, num_x);
            Grid1D y1 = new Grid1D (st_y, num_y);
            Grid1D x2 = new Grid1D (222.0F, 30);
        
            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Today;
            
            V3DataOnGrid d_grr = new V3DataOnGrid (s, date1, x1, y1);
            d_grr.InitRandom(0.34, 8.94);


            V3DataOnGrid d_grr1 = new V3DataOnGrid (s1, date2, x2, x2);
            d_grr1.InitRandom(0.1, 100.0);

            V3DataCollection d_grrrrr = new V3DataCollection(s2, date1);
            d_grrrrr.InitRandom(50, 130.0F, 132.0F, 0.0, 200.0);
            v3.Add(d_grr);
            v3.Add(d_grr1);
            v3.Add(d_grrrrr);

        }
    }
}