using SIF.Visualization.Excel.Core;
using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SIF.Visualization.Excel.ViewModel
{
    class TypeReadToImageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Rule.RuleType type = (Rule.RuleType)values[0];
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            String temp;
            switch (type)
            {
                case Rule.RuleType.DYNAMIC:
                    temp = "../icons/violations/dynamic";
                    break;
                case Rule.RuleType.SANITY:
                    temp = "../icons/violations/sanity";
                    break;
                default:
                    temp = "../icons/violations/static";
                    break;
            }
            if (values[1] is bool && !(bool)values[1])
            {
                temp = temp + "_unread";
            }

            image.UriSource = new Uri(temp + ".png", UriKind.Relative);
            image.EndInit();
            return image;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
