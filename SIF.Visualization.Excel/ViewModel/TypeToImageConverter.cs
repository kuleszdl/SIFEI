using SIF.Visualization.Excel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SIF.Visualization.Excel.ViewModel
{
    class TypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Rule.RuleType type = (Rule.RuleType)value;
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            switch (type)
            {
                case Rule.RuleType.DYNAMIC:
                    image.UriSource = new Uri("../icons/scenarios.png", UriKind.Relative);
                    break;
                case Rule.RuleType.SANITY:
                    image.UriSource = new Uri("../icons/sanity.png", UriKind.Relative);
                    break;
                case Rule.RuleType.STATIC:
                    image.UriSource = new Uri("../icons/static.png", UriKind.Relative);
                    break;
            }
            image.EndInit();
            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
