using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Shapes;
using System.Globalization;
using System.Windows.Data;
using System.Numerics;


namespace WpfApp
{
    class DetailsConverter1 : IValueConverter
    {
        public object Convert(object val, Type tt, object par, CultureInfo cul)
        {
           /* double[,] coords = (double[,]) val; */
            return $"Coord is  "; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
            throw new NotImplementedException();
            
        }

      
    }
   /* class DetailsConverter2 : IValueConverter
    {
       

    }*/
}
