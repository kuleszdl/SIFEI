using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using SIF.Visualization.Excel.Core;

namespace SIF.Visualization.Excel.ViewModel
{
    public class SingleViolationToInverseVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Violation ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}