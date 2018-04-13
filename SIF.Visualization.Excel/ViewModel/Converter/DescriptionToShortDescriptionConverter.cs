using System;
using System.Globalization;
using System.Windows.Data;
using SIF.Visualization.Excel.Properties;

namespace SIF.Visualization.Excel.ViewModel
{
    public class DescriptionToShortDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                var desc = value as string;

                if (desc.Length <= Settings.Default.DescriptionShortLength)
                    return desc;
                return desc.Substring(0, Settings.Default.DescriptionShortLength) + " ...";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}