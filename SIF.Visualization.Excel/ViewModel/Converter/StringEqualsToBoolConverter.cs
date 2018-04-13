using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace SIF.Visualization.Excel.ViewModel
{
    public class StringsNotEqualsToBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2) throw new ArgumentException("Two strings expected.");

            if (values[0] is string &&
                values[1] is string)
                return !(values[0] as string).Equals(values[1] as string);
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var result = new List<object>();
            result.Add(null);
            result.Add(null);

            return result.ToArray();
        }
    }
}