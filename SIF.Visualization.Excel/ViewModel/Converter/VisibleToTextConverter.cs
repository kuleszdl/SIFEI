using System;
using System.Globalization;
using System.Windows.Data;

namespace SIF.Visualization.Excel.ViewModel
{
    internal class VisibleToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && (bool) value)
                return "Hide in Spreadsheet";
            return "Show in Spreadsheet";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}