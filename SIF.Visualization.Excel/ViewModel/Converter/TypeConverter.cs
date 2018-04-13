using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using SIF.Visualization.Excel.Core;

namespace SIF.Visualization.Excel.ViewModel
{
    internal class TypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (Policy.PolicyType) value;
            var typeString = type.ToString();
            return typeString.ElementAt(0) + typeString.Substring(1).ToLower();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}