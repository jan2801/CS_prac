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

        public string ToString(string format)
        {
            return coor.ToString(format) + "" + electro_m_field.ToString(format);
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

        public string ToString(string format)
        {
            return step.ToString(format) + "" + number.ToString(format);
        }
    }

    


    class Program
    {

            static void DataChangedEventHandler(object source, DataChangedEventArgs args)
            {
                Console.WriteLine($"Data change happened {args}\n");
            }
            public static int Main()
            { 

                

        
                
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
               
                d_gr.measure = "my measure"; 
                V3DataCollection d_c = d_gr;
                Console.WriteLine(d_gr.ToLongString());
                

                V3MainCollection v = new V3MainCollection();
                v.DataChanged += DataChangedEventHandler;
                
                v.Add(new V3DataCollection("new_one", new DateTime()));
                v.Add(new V3DataCollection("new one 2", DateTime.Now)); //add
                v[1].measure = "this measure"; //change
               // v.AddDefaults();
                Console.WriteLine(v.Count);
                
                Console.WriteLine("this is number of elements in list\n");
               // Console.WriteLine("and under this you can find them");

                v[1] = v[0]; //replace

                v.Remove("this measure", new DateTime()); //remove
                v.Remove("new_one", new DateTime()); //remove
                v.Remove("new_one 2", new DateTime()); //remove


                //тут у нас удалилось сразу два элемента из-за replace
                



                
                

                return 0;
            

            }
    }
}
