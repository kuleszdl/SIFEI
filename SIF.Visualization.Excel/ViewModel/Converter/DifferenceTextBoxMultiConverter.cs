using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SIF.Visualization.Excel.ViewModel
{
    public class DifferenceTextBoxMultiConverter : IMultiValueConverter
    {
        /// <summary>
        ///     Calculates the text box string for the difference text box in the CreateScenarioDataField
        /// </summary>
        /// <param name="values">
        ///     [0]: difference up string
        ///     [1]: difference down string
        ///     [2]: IsChecked of difference up check box
        ///     [3]: IsChecked of difference up check box
        /// </param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>A string with the difference or a empty string</returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(values[0] is double &&
                  values[1] is double &&
                  values[2] is bool &&
                  values[3] is bool))
                return DependencyProperty.UnsetValue;

            var differenceUp = (double) values[0];
            var differenceDown = (double) values[1];
            var upIsChecked = (bool) values[2];
            var downIsChecked = (bool) values[3];

            if (differenceDown == differenceUp && !upIsChecked && !downIsChecked)
                return differenceUp.ToString();
            return string.Empty;
        }

        /// <summary>
        ///     Creats the back binding string.
        /// </summary>
        /// <param name="value">Textbox content</param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>
        ///     [0]: value as difference up string
        ///     [1]: value as difference down string
        ///     [2]: false as IsChecked of difference up check box
        ///     [3]: false as IsChecked of difference up check box
        /// </returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var myValue = 0.0;

            if (value is string)
                double.TryParse(value as string, out myValue);

            var result = new List<object>();
            result.Add(myValue);
            result.Add(myValue);
            result.Add(false);
            result.Add(false);

            return result.ToArray();
        }
    }
}