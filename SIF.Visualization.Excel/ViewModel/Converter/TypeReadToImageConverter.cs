using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using SIF.Visualization.Excel.Core;

namespace SIF.Visualization.Excel.ViewModel
{
    internal class TypeReadToImageConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var type = Policy.PolicyType.STATIC;

            try
            {
                type = (Policy.PolicyType) values[0];
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }

            var image = new BitmapImage();
            image.BeginInit();
            string temp;
            switch (type)
            {
                case Policy.PolicyType.DYNAMIC:
                    temp = "../Resources/Icons/violations/dynamic";
                    break;
                case Policy.PolicyType.SANITY:
                    temp = "../Resources/Icons/violations/sanity";
                    break;
                default:
                    temp = "../Resources/Icons/violations/static";
                    break;
            }
            if (values[1] is bool && !(bool) values[1])
                temp = temp + "_unread";

            image.UriSource = new Uri(temp + ".png", UriKind.Relative);
            image.EndInit();
            return image;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}