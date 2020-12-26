using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.ComponentModel;


namespace Lab1
{

    

    enum ChangeInfo { ItemChanged, Add, Remove, Replace };

    delegate void DataChangedEventHandler(object source, DataChangedEventArgs args);
    class V3MainCollection: IEnumerable<V3Data>
    {

        List<V3Data> v3 = new List<V3Data>();

        public event DataChangedEventHandler DataChanged;

        private void PropertyChangedEventHandler(object ob, PropertyChangedEventArgs args)
        {
            if (DataChanged != null)
                DataChanged(ob, new DataChangedEventArgs(ChangeInfo.ItemChanged, $"Property was changed"));
        }
        public int Count

        
        {
            set { }
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


        public IEnumerable<Vector2> ExGrid
        {
            get
            {
                var dg = v3.Where(obj => obj is V3DataOnGrid)
                                        .Select(v3_b);
                var dc = v3.Where(obj => obj is V3DataCollection)
                                        .Select(v3_b);

                var qgrid = from data in dg from vector in data select vector.coor;

                var qvcollect = from data in dc from vector in data select vector.coor;

                return qvcollect.Except(qgrid).Distinct();
            }
        }

        public void Add(V3Data m)
        {
            int c = v3.Count();
            v3.Add(m);

            m.PropertyChanged += PropertyChangedEventHandler;
            if (DataChanged != null)
                DataChanged(this, new DataChangedEventArgs(ChangeInfo.Add, $". New elemenent was added, it was {c} elements, but now {Count} elements.\n"));
        }

        private V3DataCollection v3_b(V3Data elem)
        {
            return elem is V3DataOnGrid ? (V3DataCollection) (elem as V3DataOnGrid) : elem as V3DataCollection;
        }

        public V3Data this[int index]
        {
            get
            {
                if (v3.Count < index)
                    throw new IndexOutOfRangeException();


                return v3[index];
            }
       
            set
            {
                v3[index] = value;
                if (DataChanged != null)
                {
                    DataChangedEventArgs d = new DataChangedEventArgs(ChangeInfo.Replace, $". Element {index} replace happened.\n");
                    DataChanged(this, d);
                }
            }
        }


        public bool Remove(string id, DateTime date)
        {
            bool k = false;
            int c = v3.Count();
            
            foreach (V3Data el in v3.ToList())
            {
                if (el.measure == id && el.date == date)
                {
                    el.PropertyChanged -= PropertyChangedEventHandler;
                }
            }
            
            foreach (V3Data el in v3.ToList())
            {
                if (el.measure == id && el.date == date)
                {
                    
                    v3.Remove(el);
                    if (DataChanged != null)
                        DataChanged(this, new DataChangedEventArgs(ChangeInfo.Remove, $". Element was removed, it was {c} elements, but now {Count} elements.\n"));
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

        public float RMin (Vector2 v)
        {

            

            var q1 = v3.Select(v3_b);
            var q2 = from d in q1 from vector in d select vector;

  

            DataItem min = q2.OrderBy(item => Vector2.Distance(item.coor, v)).First();
            return Vector2.Distance(min.coor, v);

            //выбираем минимум
        }

        public DataItem RMinDataItem (Vector2 v)
        {
            var q1 = v3.Select(v3_b);
            var q2 = from d in q1 from vector in d select vector;

  

            return q2.OrderBy(item => Vector2.Distance(item.coor, v)).First();
        }

        


        public string ToLongString(string format)
        {
            string st = "";
            foreach (V3Data elem in v3.ToList())
            {
                st += elem.ToLongString(format);
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

      

         
            string defstr = "default";
            
            

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


            // V3DataOnGrid
            
        
  
            // V3DataCollection
            v3.Add((V3DataCollection)(v3[0] as V3DataOnGrid));

   
           


            v3.Add(new V3DataCollection(defstr, date1));
            v3.Add(new V3DataOnGrid("", new DateTime(), new Grid1D(0, 0), new Grid1D(0, 0)));

        }
    }
}