﻿using SIF.Visualization.Excel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                                         where violation.ViolationState.Equals(violationState) && violation.Cell.Equals(cell) && violation.Kind.Equals(Violation.ViolationKind.SINGLE)
                                         select violation).ToList();
            List<Violation> groupViolations = (from violation in list where violation.Kind.Equals(Violation.ViolationKind.GROUP) select violation).ToList();
            foreach (GroupViolation groupViolation in groupViolations)
            {
                foreach (Violation violation in groupViolation.Violations)
                {
                    if (violation.Cell.Equals(cell))
                    {
                        sameCells.Add(violation);
                    }
                }
            }
            if (sameCells.Count <= 1)
            {
                object[] objs = new object[2];
                objs[0] = values[2];
                objs[1] = values[3];
                return (BitmapImage)conv.Convert(objs, typeof(System.Windows.Media.Imaging.BitmapImage), parameter, culture);
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