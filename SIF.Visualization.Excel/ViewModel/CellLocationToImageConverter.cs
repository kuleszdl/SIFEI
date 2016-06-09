using SIF.Visualization.Excel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SIF.Visualization.Excel.ViewModel
{
    class CellLocationToImageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TypeReadToImageConverter conv = new TypeReadToImageConverter();
            CellLocation cell = (CellLocation)values[0];
            ViolationType violationState = (ViolationType)values[1];

            List<Violation> list = null;
            switch (violationState)
            {
                default:
                    list = DataModel.Instance.CurrentWorkbook.Violations.ToList();
                    break;
                case ViolationType.LATER:
                    list = DataModel.Instance.CurrentWorkbook.LaterViolations.ToList();
                    break;
                case ViolationType.IGNORE:
                    list = DataModel.Instance.CurrentWorkbook.IgnoredViolations.ToList();
                    break;
                case ViolationType.SOLVED:
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
                return (BitmapImage)conv.Convert(objs, typeof(BitmapImage), parameter, culture);
            }
            else
            {
                return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
