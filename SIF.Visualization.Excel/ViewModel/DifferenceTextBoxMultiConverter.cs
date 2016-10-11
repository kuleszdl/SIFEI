using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace SIF.Visualization.Excel.ViewModel
{
    public class DifferenceTextBoxMultiConverter : IMultiValueConverter
    {
        /// <summary>
        /// Calculates the text box string for the difference text box in the CreateScenarioDataField
        /// </summary>
        /// <param name="values">
        /// [0]: difference up string
        /// [1]: difference down string
        /// [2]: IsChecked of difference up check box
        /// [3]: IsChecked of difference up check box</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>A string with the difference or a empty string</returns>
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(values[0] is double &&
                values[1] is double &&
                values[2] is Boolean &&
                values[3] is Boolean))
            {
                return DependencyProperty.UnsetValue;
            }

            var differenceUp = (double) values[0];
            var differenceDown = (double) values[1];
            var upIsChecked = (Boolean) values[2];
            var downIsChecked = (Boolean) values[3];

            if (differenceDown == differenceUp && !upIsChecked && !downIsChecked)
            {
                return differenceUp.ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Creats the back binding string.
        /// </summary>
        /// <param name="value">Textbox content</param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>
        /// [0]: value as difference up string
        /// [1]: value as difference down string
        /// [2]: false as IsChecked of difference up check box
        /// [3]: false as IsChecked of difference up check box</returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            double myValue = 0.0;

            if (value is String)
            {
                Double.TryParse(value as String, out myValue);
            }

            var result = new List<object>();
            result.Add(myValue);
            result.Add(myValue);
            result.Add(false);
            result.Add(false);

            return result.ToArray();
        }
    }
}
