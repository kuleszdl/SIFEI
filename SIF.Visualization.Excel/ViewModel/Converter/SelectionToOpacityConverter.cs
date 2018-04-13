using System;
using System.Globalization;
using System.Windows.Data;

namespace SIF.Visualization.Excel.ViewModel
{
    public class SelectionToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool && (bool) value ? 0.5 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}