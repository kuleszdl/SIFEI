using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using SIF.Visualization.Excel.Core;

namespace SIF.Visualization.Excel.ViewModel
{
    public class InverseIntToVisibilityMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var result = Visibility.Visible;
            foreach (var v in values)
                if (v is ObservableCollection<Cell> && (v as ObservableCollection<Cell>).Count > 0)
                    result = Visibility.Collapsed;

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}