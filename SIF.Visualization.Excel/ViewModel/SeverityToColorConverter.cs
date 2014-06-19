using SIF.Visualization.Excel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SIF.Visualization.Excel.ViewModel
{
    public class SeverityToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal number = (decimal)value;

            var maximumSeverity = DataModel.Instance.CurrentWorkbook.Violations.Max(p => p.Severity);

            number = number / maximumSeverity;

            decimal startR = 255;
            decimal startG = 215;
            decimal startB = 0;

            decimal endR = 192;
            decimal endG = 0;
            decimal endB = 0;

            decimal diffR = endR - startR;
            decimal diffG = endG - startG;
            decimal diffB = endB - startB;

            return new Color() { A = 255, R = (byte)(startR + number * diffR), G = (byte)(startG + number * diffG), B = (byte)(startB + number * diffB) };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
