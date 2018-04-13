using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using SIF.Visualization.Excel.Core;

namespace SIF.Visualization.Excel.ViewModel
{
    internal class SeverityIgnoreToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vio = (Violation) value;
            if (vio != null)
                switch (vio.ViolationState)
                {
                    case ViolationState.IGNORE:
                        return Colors.LightGray;
                    case ViolationState.SOLVED:
                        return Color.FromRgb(255, 255, 255);
                }
            // Color for others
            var maximumSeverity = 500;

            var severity = 0 / maximumSeverity;

            var startR = 255.0;
            var startG = 215.0;
            var startB = 0.0;

            var endR = 255.0;
            var endG = 50.0;
            var endB = 50.0;

            var diffR = endR - startR;
            var diffG = endG - startG;
            var diffB = endB - startB;


            return new Color
            {
                A = 255,
                R = (byte) (startR + severity * diffR),
                G = (byte) (startG + severity * diffG),
                B = (byte) (startB + severity * diffB)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}