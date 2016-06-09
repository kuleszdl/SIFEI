using System;
using System.Windows.Data;

namespace SIF.Visualization.Excel.ViewModel
{
    public class StringIsNotEmptyToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Converts a string to visibility if the string is empty or null
        /// </summary>
        /// <param name="value">string of a textbox</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || (value is string && (value as string)==String.Empty))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
