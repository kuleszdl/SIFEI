using SIF.Visualization.Excel.Core;
using System;
using System.Linq;
using System.Windows.Data;

namespace SIF.Visualization.Excel.ViewModel
{
    class TypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Core.Policy.PolicyType type = (Core.Policy.PolicyType)value;
            String typeString = type.ToString();
            return typeString.ElementAt(0) + typeString.Substring(1).ToLower();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
