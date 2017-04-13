using SIF.Visualization.Excel.Core;
using System;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SIF.Visualization.Excel.ViewModel
{
    class TypeReadToImageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Core.Policy.PolicyType type = Core.Policy.PolicyType.STATIC;

            try {
                type = (Core.Policy.PolicyType)values[0];
            } catch (Exception e) {
                Debug.WriteLine(e.StackTrace);
            }

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            String temp;
            switch (type)
            {
                case Core.Policy.PolicyType.DYNAMIC:
                    temp = "../Resources/Icons/violations/dynamic";
                    break;
                case Core.Policy.PolicyType.SANITY:
                    temp = "../Resources/Icons/violations/sanity";
                    break;
                default:
                    temp = "../Resources/Icons/violations/static";
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
