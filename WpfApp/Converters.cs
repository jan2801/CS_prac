using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;
using System.Globalization;
using System.Windows.Data;
using System.Numerics;
using ClassLibrary;


namespace WpfApp
{
    class DetailsConverter1 : IValueConverter
    {
        public object Convert(object val, Type tt, object par, CultureInfo cul)
        {
            Vector2 coords = (Vector2) val; 
            return $"Coord is {coords}"; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
            throw new NotImplementedException();
            
        }

      
    }

    class DetailsConverter2 : IValueConverter
    {
        public object Convert(object val, Type tt, object par, CultureInfo cul)
        {
            double v = (double)val;
            return $"Value is {v}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
            throw new NotImplementedException();

        }


    }


    class DetailsConverter3 : IValueConverter
    {
        public object Convert(object val, Type tt, object par, CultureInfo cul)
        {
            
            V3DataOnGrid v = (V3DataOnGrid)val;



            if (v != null)
            {
                return $"OXGrid {v.x.step} with number of {v.x.number}";
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
            throw new NotImplementedException();

        }


    }

    class DetailsConverter4 : IValueConverter
    {
        public object Convert(object val, Type tt, object par, CultureInfo cul)
        {
            V3DataOnGrid v = (V3DataOnGrid)val;
            if (v != null)
            {
                return $"OYGrid {v.y.step} with number of {v.y.number}";
            }
            return null;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
            throw new NotImplementedException();

        }


    }

    class DetailsConverter5 : IValueConverter
    {
        public object Convert(object val, Type tt, object par, CultureInfo cul)
        {
            return val;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();

        }


}



    /* class DetailsConverter2 : IValueConverter
     {


     }*/
}
