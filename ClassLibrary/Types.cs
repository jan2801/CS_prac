using System;
using System.Numerics;



namespace ClassLibrary
{
    public class DataItem
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

    public class Grid1D
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


    
}
