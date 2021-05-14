using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Windows;


namespace ClassLibrary
{

    

    public enum ChangeInfo { ItemChanged, Add, Remove, Replace };

    

    public delegate void DataChangedEventHandler(object source, DataChangedEventArgs args);

   
    [Serializable]
    public class V3MainCollection : IEnumerable<V3Data>, INotifyCollectionChanged, INotifyPropertyChanged
    {

        public bool changes
        {
            set;
            get;
        }

        
        
        private void V3MainCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public interface ISerializable
        {
            void GetObjectData(SerializationInfo info, StreamingContext context);
        }

    


        public List<V3Data> v3 = new List<V3Data>();
        

        [field: NonSerialized]
        public event DataChangedEventHandler DataChanged;

        [field: NonSerialized]
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;



        private void PropertyChangedHandler(object ob, PropertyChangedEventArgs args)
        {
            CollectionChangedEvent(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            if (DataChanged != null)
                DataChanged(ob, new DataChangedEventArgs(ChangeInfo.ItemChanged, $"Property was changed"));
        }

        public void CollectionChangedEvent(object ob, NotifyCollectionChangedEventArgs args)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        public int Count
        {
            set { }
            get { return v3.Count; }
        }
    
        public double FromZeroToPoint
        {
            get
            {
                if (this.Count > 0)
                {
                    var query1 = (from elem in (from item in this.v3
                                              where item is V3DataCollection
                                              select (V3DataCollection)item)
                                from dti in elem
                                select (double)dti.coor.Length());
                    var query2 = (from elem in (from item in this.v3
                                                where item is V3DataOnGrid
                                                select (V3DataOnGrid)item)
                                  select (Math.Sqrt(Math.Pow((elem.x.step * elem.x.number), 2) + Math.Pow((elem.y.step * elem.y.number), 2))));
                    return query1.Concat(query2).Max();
                    
                    
                }
                return 0;
               
             
            }
            

        }
        private void CollectionChangedHandler(object source, NotifyCollectionChangedEventArgs args)
        {
            changes = true;
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("FromZeroToPoint"));

            


        }

        public V3MainCollection()
        {
            CollectionChanged += CollectionChangedHandler;
        }

        

        public IEnumerator<V3Data> GetEnumerator()
        {
            return v3.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public bool IsChanged
        {
            get
            {
                return changes;
            }
            set
            {
                changes = value;
                PropertyChangedEventArgs pc = new PropertyChangedEventArgs("Changes were made");
                PropertyChanged?.Invoke(this, pc);
            }
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

            //m.PropertyChanged += PropertyChangedHandler;
            if (DataChanged != null)
                DataChanged(this, new DataChangedEventArgs(ChangeInfo.Add, $". New elemenent was added, it was {c} elements, but now {Count} elements.\n"));
            CollectionChangedEvent(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("Collection was changed"));

            
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
                CollectionChangedEvent(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }

            
        }

        public void Remove(int ind)
        {
            int i = ind;
            v3.RemoveAt(i);
         
            CollectionChangedEvent(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            DataChanged?.Invoke(this, new DataChangedEventArgs(ChangeInfo.Remove, $"it is {v3.Count} items now"));
        }
        public bool Remove(string id, DateTime date)
        {
            bool k = false;
            int c = v3.Count();
            V3Data ell = null;
            
            foreach (V3Data el in v3)
            {
                //if (el != null)
                    if (el.measure == id && el.date == date)
                    {
                        el.PropertyChanged -= PropertyChangedHandler;
                    }
            }
            
            foreach (V3Data el in v3)
            {
                //if (el != null)
                    if (el.measure == id && el.date == date)
                    {

                        ell = el;
                        
                        if (DataChanged != null)
                            DataChanged(this, new DataChangedEventArgs(ChangeInfo.Remove, $". Element was removed, it was {c} elements, but now {Count} elements.\n"));
                        k = true;
                    }
            }
            if (ell != null)
                v3.Remove(ell);

            CollectionChangedEvent(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

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


        public void Save(string filename)
        {
            FileStream f = null;
            try
            {
                f = new FileStream(filename, FileMode.OpenOrCreate);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(f, v3);
                changes = false;

            }

            /*catch (Exception e)
            {
                Console.WriteLine($"Exception with {e.Message} was cathed");
            } */
            finally
            {
                if (f != null)
                    f.Close();
            }

        }

        public void Load(string filename)
        {
            FileStream f = null;
            try
            {
                f = File.OpenRead(filename);
                BinaryFormatter formatter = new BinaryFormatter();
                List<V3Data>  des_data = (List<V3Data>)formatter.Deserialize(f);
                v3 = des_data;
            }

            catch (Exception e)
            {
                Console.WriteLine($"Exception with {e.Message} was cathed");
                throw;
            } 
            finally
            {
                if (f != null)
                    f.Close();
                CollectionChangedEvent(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
            

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
            this.Add(d_grr);
            this.Add(d_grr1);
            this.Add(d_grrrrr);


            // V3DataOnGrid
            
        
  
            // V3DataCollection
            this.Add((V3DataCollection)(v3[0] as V3DataOnGrid));

   
           


            this.Add(new V3DataCollection(defstr, date1));
            this.Add(new V3DataOnGrid("", new DateTime(), new Grid1D(0, 0), new Grid1D(0, 0)));
            CollectionChangedEvent(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

        }
    }
}