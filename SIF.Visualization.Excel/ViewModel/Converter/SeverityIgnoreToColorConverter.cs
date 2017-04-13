using SIF.Visualization.Excel.Core;
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SIF.Visualization.Excel.ViewModel
{
    class SeverityIgnoreToColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            Core.Violation vio = (Core.Violation) value;
            if (vio != null) {
            // Fixed colors
                switch (vio.ViolationState)
                {
                    case ViolationState.IGNORE:
                        return Colors.LightGray;
                    case ViolationState.SOLVED:
                        return Color.FromRgb(255, 255, 255);
                }
            }
            // Color for others
            var maximumSeverity = 500;

            var severity = 0 / maximumSeverity;

            double startR = 255.0;
            double startG = 215.0;
            double startB = 0.0;

            double endR = 255.0;
            double endG = 50.0;
            double endB = 50.0;

            double diffR = endR - startR;
            double diffG = endG - startG;
            double diffB = endB - startB;


            return new Color() {A=255, R = (byte)(startR + severity * diffR), G = (byte)(startG + severity * diffG), B = (byte)(startB + severity * diffB) };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
