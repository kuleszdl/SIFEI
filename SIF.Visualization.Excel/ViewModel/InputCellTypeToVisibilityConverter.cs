using SIF.Visualization.Excel.ScenarioCore;
using System;
using System.Windows;
using System.Windows.Data;

namespace SIF.Visualization.Excel.ViewModel
{
    public class InputCellTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is CellDefinitionType && ((CellDefinitionType)value) == CellDefinitionType.Input)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
