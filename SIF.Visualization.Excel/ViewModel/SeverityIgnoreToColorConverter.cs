using SIF.Visualization.Excel.Core;
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace SIF.Visualization.Excel.ViewModel
{
    class SeverityIgnoreToColorConverter : IMultiValueConverter
    {

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
            {
                return null;
            }
            decimal severity = (decimal)values[0];
            ViolationType violationType = (ViolationType)values[1];

            // Fixed colors
            switch (violationType)
            {
                case ViolationType.IGNORE:
                    return Colors.LightGray;
                case ViolationType.SOLVED:
                    return Color.FromRgb(255, 255, 255);
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

            return new Color() {A=255, R = (byte)(startR + severity * diffR), G = (byte)(startG + severity * diffG), B = (byte)(startB + severity * diffB) };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
