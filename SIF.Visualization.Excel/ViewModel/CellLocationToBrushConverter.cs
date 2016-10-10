using SIF.Visualization.Excel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SIF.Visualization.Excel.ViewModel
{
    class CellLocationToBrushConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SeverityIgnoreToColorConverter conv = new SeverityIgnoreToColorConverter();

            ViolationType violationState = (ViolationType)values[0];
            ICollection<Violation> violations = values[1] as ICollection<Violation>;
            SolidColorBrush brush = new SolidColorBrush();
            brush.Color= Colors.White;
            if (violations != null && violations.Any())
            {
                decimal maxSeverity = violations.Max(vi => vi.Severity);
                decimal minSeverity = violations.Min(vi => vi.Severity);
                object[] objs = new object[2];
                objs[0] = maxSeverity;
                objs[1] = violationState;
                Color maxColor = (Color)conv.Convert(objs, typeof(Color), parameter, culture);
                objs[0] = minSeverity;
                //Color minColor = (Color)conv.Convert(objs, typeof(Color), parameter, culture);
                brush.Color = maxColor;
            }
            return brush;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
