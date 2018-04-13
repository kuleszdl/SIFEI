using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using SIF.Visualization.Excel.Core;

namespace SIF.Visualization.Excel.ViewModel
{
    public class SingleViolationToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Violation ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}