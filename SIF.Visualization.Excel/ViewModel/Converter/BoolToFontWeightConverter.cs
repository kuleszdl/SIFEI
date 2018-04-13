using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SIF.Visualization.Excel.ViewModel
{
    internal class BoolToFontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && !(bool) value) return FontWeights.Bold;
            return FontWeights.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}