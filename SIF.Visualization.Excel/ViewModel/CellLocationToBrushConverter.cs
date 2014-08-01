using SIF.Visualization.Excel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            CellLocation cell = (CellLocation)values[0];
            Violation.ViolationType violationState = (Violation.ViolationType)values[1];

            List<Violation> list = null;
            switch (violationState)
            {
                default:
                    list = DataModel.Instance.CurrentWorkbook.Violations.ToList();
                    break;
                case Violation.ViolationType.LATER:
                    list = DataModel.Instance.CurrentWorkbook.LaterViolations.ToList();
                    break;
                case Violation.ViolationType.IGNORE:
                    list = DataModel.Instance.CurrentWorkbook.IgnoredViolations.ToList();
                    break;
                case Violation.ViolationType.SOLVED:
                    list = DataModel.Instance.CurrentWorkbook.SolvedViolations.ToList();
                    break;
            }
            List<Violation> sameCells = (from violation in list
                                         where violation.ViolationState.Equals(violationState) && violation.Cell.Equals(cell)
                                         select violation).ToList();
            if (sameCells.Count <= 1)
            {
                object[] objs = new object[2];
                objs[0] = values[2];
                objs[1] = values[3];
                return new SolidColorBrush((Color)conv.Convert(objs, typeof(System.Windows.Media.Color), parameter, culture));
            }
            else
            {
                decimal maxSeverity = sameCells.Max(vi => vi.Severity);
                decimal minSeverity = sameCells.Min(vi => vi.Severity);
                object[] objs = new object[2];
                objs[0] = maxSeverity;
                objs[1] = violationState;
                Color maxColor = (Color)conv.Convert(objs, typeof(System.Windows.Media.Color), parameter, culture);
                objs[0] = minSeverity;
                Color minColor = (Color)conv.Convert(objs, typeof(System.Windows.Media.Color), parameter, culture);
                LinearGradientBrush brush = new LinearGradientBrush();
                brush.StartPoint = new Point(0, 1);
                brush.EndPoint = new Point(0, 0);
                brush.GradientStops.Add(new GradientStop(minColor, 0));
                brush.GradientStops.Add(new GradientStop(maxColor, 1));
                return brush;
            }

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
