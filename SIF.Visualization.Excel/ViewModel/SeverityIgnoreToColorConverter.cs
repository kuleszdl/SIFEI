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
    class SeverityIgnoreToColorConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            decimal severity = (decimal)values[0];
            Violation.ViolationType violationType = (Violation.ViolationType)values[1];

            // Fixed colors
            switch (violationType)
            {
                case Violation.ViolationType.IGNORE:
                    return Colors.LightGray;
                case Violation.ViolationType.SOLVED:
                    return Color.FromRgb(20, 210, 0);
            }

            // Color for others
            var maximumSeverity = 500;

            severity = severity / maximumSeverity;

            decimal startR = 255;
            decimal startG = 215;
            decimal startB = 0;

            decimal endR = 255;
            decimal endG = 50;
            decimal endB = 50;

            decimal diffR = endR - startR;
            decimal diffG = endG - startG;
            decimal diffB = endB - startB;

            return new Color() { A = 255, R = (byte)(startR + severity * diffR), G = (byte)(startG + severity * diffG), B = (byte)(startB + severity * diffB) };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
