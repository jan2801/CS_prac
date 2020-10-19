using System;
using System.Numerics;



namespace Lab1
{

    struct DataItem
    {
        public System.Numerics.Vector2 coor;
        public double electro_m_field;

        public DataItem(System.Numerics.Vector2 vec, double field)
        {
            coor = vec;
            electro_m_field = field;
        }

        
        public override string ToString()
        {
            return coor.ToString() + "" + electro_m_field.ToString();
        }
    }

    struct Grid1D
    {

        public float step;
        public int number;

        public Grid1D(float st, int num)
        {
            step = st;
            number = num;
        }
        
        public override string ToString()
        {
            return step.ToString() + "" + number.ToString();
        }
    }


    class Program
    {
            public static int Main()
            { 
                Console.WriteLine("TASK №1");
                string s = "sdsafasf";
                float st_x = 1.0F;
                int num_x = 10;
                float st_y = 2.0F;
                int num_y = 15;
                Grid1D x1 = new Grid1D (st_x, num_x);
                Grid1D y1 = new Grid1D (st_y, num_y);
                DateTime date1 = DateTime.Today;
                
                V3DataOnGrid d_gr = new V3DataOnGrid(s, date1, x1, y1);
                d_gr.InitRandom(34.0, 67.0);
                V3DataCollection d_c = d_gr;
                Console.WriteLine(d_gr.ToLongString());
                
                
                Console.WriteLine("TASK №2");

                V3MainCollection v = new V3MainCollection();
                v.AddDefaults();
                Console.WriteLine(v.Count);
                Console.WriteLine("this is number of elements in list");
                Console.WriteLine("and under this you can find them");

                Console.WriteLine(v.ToString());

                

                Console.WriteLine("TASK №3");
                Vector2 vect = new Vector2(23.0F, 14.0F);
                int counter = 1;
                foreach(V3Data obj in v)
                {
                    Console.WriteLine("The nearest points for object number " + counter + " are: ");
                    for (int i = 0; i < obj.Nearest(vect).Length; i++)
                    {
                        Console.WriteLine("" + i +". (" + obj.Nearest(vect)[i].X + " ; " + obj.Nearest(vect)[i].Y + ")");
                    }
                    counter ++;

                }

                return 0;

        }
    }
}
