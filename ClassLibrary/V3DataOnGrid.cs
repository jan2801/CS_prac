using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Globalization;
using System.Runtime.Serialization;
using System.IO;



namespace ClassLibrary
{


    [Serializable]
    [KnownType(typeof(Grid1D))]
    public class V3DataOnGrid: V3Data, IEnumerable<DataItem>
    {
        public Grid1D x { get; set; }
        public Grid1D y { get; set; }
        public double[,] values { get; set; }


        public V3DataOnGrid(string id, DateTime dt, Grid1D xx, Grid1D yy): base(id, dt)
        {
            measure = id;
            date = dt;
            x = xx;
            y = yy;
            values = new double[x.number, y.number];
        }

        public V3DataOnGrid(string filename): base("", new DateTime())
        {
            CultureInfo CI = new CultureInfo("ru-RU");
            FileStream fstream = null;
            try
            {


                    fstream = new FileStream(filename, FileMode.Open);
                
                    // преобразуем строку в байты
                    byte[] array = new byte[fstream.Length];
                    // считываем данные
                    fstream.Read(array, 0, array.Length);
                    // декодируем байты в строку
                    string textFromFile = System.Text.Encoding.Default.GetString(array);
                    Console.WriteLine($"Text from file: {textFromFile}");

                    float st;
                    int   n;
                
                    

                    
                    string[] words = textFromFile.Split(' ');
                    
                    measure = words[0];
        
                    
                    st = (float) Convert.ToDouble(words[2], CI);
            
                    n = Convert.ToInt32(words[3], CI);
                    
                    date = Convert.ToDateTime(words[1], CI);
                    Console.WriteLine(date);
                    Console.WriteLine("lol");
                    x = new Grid1D(st, n);
                    st = (float) Convert.ToDouble(words[4], CI);
                    n = Convert.ToInt32(words[5], CI);
                    
        
                    y = new Grid1D(st, n);
                    values = new double[x.number, y.number];
                    for (int i = 0; i < x.number; i++)
                    {
                        for (int j = 0; j < y.number; j++)
                        {
                            values[i, j] = Convert.ToDouble(words[6 + i * x.number + j], CI);
                        }
                    }
                      
                       
                        

                    

            


                

            }

            catch (Exception ex)
            {
                
                
                Console.WriteLine(ex.Message);
           
            }

            finally
            {
                if (fstream != null)
                {
                    fstream.Close(); 
                }
            }
        }

        public void InitRandom(double minValue, double maxValue)
        {
            Random rand = new Random();
            values = new double [x.number, y.number];
            double f;
            for (int i = 0; i < x.number; i++)
                for (int j = 0; j < y.number; j++)
                {

                    f = (double) rand.NextDouble() * (maxValue - minValue) + minValue;
                    values[i, j] = f;
                }
        }
    

        public static implicit operator V3DataCollection(V3DataOnGrid v)
        {
            if (v != null)
            {
                V3DataCollection col = new V3DataCollection(v.measure, v.date);

                Vector2 c;
                double elfield;
                for (int i = 0; i < v.x.number; i++)
                    for (int j = 0; j < v.y.number; j++)
                    {
                        c.X = v.x.step * i; c.Y = v.y.step * j;


                        elfield = v.values[i, j];

                        col.lst_d.Add(new DataItem(c, elfield));
                    }
                return col;
            }
            return null;
        }

        public override Vector2[] Nearest(Vector2 v)
        {
            Vector2 new_coor;
            DataItem el;
            float ebs = 0.0000000001F;
            float minim;
            
            List<Vector2> l = new List<Vector2>();
            new_coor = new Vector2(0, 0);

        
            minim = Vector2.Distance(new_coor, v);
            
            for (int i = 0; i < x.number; i++)
                for (int j = 0; j < y.number; j++)
                {
                    new_coor = new Vector2(x.step * i, y.step * j);
                    el = new DataItem(new_coor, values[i, j]);
                    if ((Math.Abs(Vector2.Distance(el.coor, v) - minim)) < ebs)
                    {
                        l.Add(el.coor);
                    }
                    else if (Vector2.Distance(el.coor, v) < minim)
                    {
                        l.Clear();
                        minim = Vector2.Distance(el.coor, v);
                        l.Add(el.coor);

                    }
                }
            Vector2[] shortest = l.ToArray();
            //обычно в массив будет входить один элемент, но если расстояние до заданной точки от каких-то узлов одиаковое, то возможно и вхождение нескольких
            return shortest;
        }

        public override string ToString()
        {
            string st_1 = "V3DataOnGrid";
            return (st_1 + " " + measure + " " + date.ToString() + "  " + x.ToString() + " " +  y.ToString() + "\n");
        }

        public override string ToLongString()
        {
            string st_1 = "V3DataOnGrid";
            st_1 += " " + measure + " " + date.ToString() + "  " + x.ToString() + " " +  y.ToString() + "\n";
            for (int i = 0; i < x.number; i++)
                for (int j = 0; j < y.number; j++)
                {
                    st_1 += "values[" + i * x.step + "," + j * y.step + "] = "  + values[i, j]  + "\n";
                }
            return st_1;
        }

        public override string ToLongString(string format)
        {
            string st_1 = "V3DataOnGrid";
            st_1 += " " + measure + " " + date.ToString(format) + "  " + x.ToString(format) + " " +  y.ToString(format) + "\n";
            Console.WriteLine(st_1);
            for (int i = 0; i < x.number; i++)
                for (int j = 0; j < y.number; j++)
                {
                    st_1 += "values[" + i * x.step + "," + j * y.step + "] = "  + values[i, j]  + "\n";
                }
            return st_1;
        }

        public IEnumerator<DataItem> GetEnumerator()
        {
            List<DataItem> item_list = new List<DataItem>();
            DataItem di;
            Vector2 coor;
            double val;
            for (int i = 0; i < x.number; i++)
                for (int j = 0; j < y.number; j++)
                {
                    coor.X = i;
                    coor.Y = j;
                    val = values[i, j];
                   
                    di = new DataItem(coor, val);
                    yield return di;
                }
            //return item_list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            List<DataItem> item_list = new List<DataItem>();
            DataItem di;
            Vector2 coor;
            double val;
            for (int i = 0; i < x.number; i++)
                for (int j = 0; j < y.number; j++)
                {
                    coor.X = i;
                    coor.Y = j;
                    val = values[i, j];
                   
                    di = new DataItem(coor, val);
                    yield return di;
                }
           // return item_list.GetEnumerator();
        }


        
    }
}