using System;
using System.Windows.Data;

namespace SIF.Visualization.Excel.ViewModel
{
    public class DescriptionToShortDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                var desc = value as string;

                if (desc.Length <= Properties.Settings.Default.DescriptionShortLength)
                {
                    return desc;
                }
                else
                {
                    return desc.Substring(0, Properties.Settings.Default.DescriptionShortLength) + " ...";
                }
            }
            else
            {
                return String.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
