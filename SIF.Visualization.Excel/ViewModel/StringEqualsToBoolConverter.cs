using System;
using System.Collections.Generic;
using System.Windows.Data;

namespace SIF.Visualization.Excel.ViewModel
{
    public class StringsNotEqualsToBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length != 2) throw new ArgumentException("Two strings expected.");

            if (values[0] is String &&
                values[1] is String)
            {
                return !(values[0] as String).Equals(values[1] as String);
            }
            else
            {
                return false;
            }
            
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            var result = new List<object>();
            result.Add(null);
            result.Add(null);

            return result.ToArray();
        }
    }
}
