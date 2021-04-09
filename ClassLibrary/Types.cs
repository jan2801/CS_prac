using System;
using System.Numerics;
using System.Runtime.Serialization;



namespace ClassLibrary
{
    [Serializable]
    public class DataItem: ISerializable
    {
        public System.Numerics.Vector2 coor
        {
            get;
            set;

        }
        public double electro_m_field
        {
            get;
            set;
        }

        public DataItem(System.Numerics.Vector2 vec, double field)
        {
            coor = vec;
            electro_m_field = field;
        }

        public DataItem()
        {
        }

        public override string ToString()
        {
            return coor.ToString() + "" + electro_m_field.ToString();
        }

        public string ToString(string format)
        {
            return coor.ToString(format) + "" + electro_m_field.ToString(format);
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("coor_x", coor.X);
            info.AddValue("coor_y", coor.Y);
            info.AddValue("field", electro_m_field);

        }
        
        public DataItem(SerializationInfo info, StreamingContext context)
        {
            float x = info.GetSingle("coor_x");
            float y = info.GetSingle("coor_y");
            coor = new Vector2(x, y);
            electro_m_field = (double)info.GetDouble("field");
        }
    }

    [Serializable]
    public class Grid1D: ISerializable
    {

        public float step;
        public int number;

        public Grid1D(float st, int num)
        {
            step = st;
            number = num;
        }

        public Grid1D(SerializationInfo info, StreamingContext context)
        {
            step = info.GetSingle("st");
            number = info.GetInt32("numb");
        }




        public override string ToString()
        {
            return step.ToString() + "" + number.ToString();
        }

        public string ToString(string format)
        {
            return step.ToString(format) + "" + number.ToString(format);
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("st", step);
            info.AddValue("numb", number);
        }
    }


    
}
