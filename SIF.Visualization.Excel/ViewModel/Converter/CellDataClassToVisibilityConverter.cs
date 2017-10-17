using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using SIF.Visualization.Excel.Core.Scenarios;

namespace SIF.Visualization.Excel.ViewModel
{
    public class CellDataClassToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ScenarioData || value is ConditionData)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}