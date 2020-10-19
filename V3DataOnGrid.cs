using System;
using System.Collections.Generic;
using System.Numerics;


namespace Lab1
{



    class V3DataOnGrid: V3Data
    {
        public Grid1D x { get; set; }
        public Grid1D y { get; set; }
        public double[,] values { get; set; }


        public V3DataOnGrid(string id, DateTime dt, Grid1D xx, Grid1D yy): base(id, dt)
        {
            meas_ident = id;
            d_time = dt;
            x = xx;
            y = yy;
            values = new double[x.number, y.number];
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
            V3DataCollection col = new V3DataCollection(v.meas_ident, v.d_time);
            Vector2 c;
            double elfield;
            for (int i = 0; i < v.x.number; i++)
                for (int j = 0; j < v.y.number; j++)
                {
                    c.X = v.x.step * i;c.Y = v.y.step * j;


                    elfield = v.values[i, j];

                    col.lst_d.Add(new DataItem(c, elfield));
                }
            return col;
        }

        public override Vector2[] Nearest(Vector2 v)
        {
            Vector2 new_coor;
            DataItem el;
            float ebs = 0.00001F;
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
            return (st_1 + " " + meas_ident + " " + d_time.ToString() + "  " + x.ToString() + " " +  y.ToString() + "\n");
        }

        public override string ToLongString()
        {
            string st_1 = "V3DataOnGrid";
            st_1 += " " + meas_ident + " " + d_time.ToString() + "  " + x.ToString() + " " +  y.ToString() + "\n";
            for (int i = 0; i < x.number; i++)
                for (int j = 0; j < y.number; j++)
                {
                    st_1 += "values[" + i * x.step + "," + j * y.step + "] = "  + values[i, j]  + "\n";
                }
            return st_1;
        }

        
    }
}