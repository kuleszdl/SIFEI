using System;
using System.Windows.Data;

namespace SIF.Visualization.Excel.ViewModel
{
    class VisibleToTextConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool && (bool)value)
            {
                return "Hide in Spreadsheet";
            }
            else
            {
                return "Show in Spreadsheet";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
