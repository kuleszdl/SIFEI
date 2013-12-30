using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SIF.Visualization.Excel.ViewModel
{
    public class BooleanNORConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var result = true;

            foreach (var b in values)
            {
                if (b is bool && (bool)b)
                {
                    result = false;
                }
                else if (!(b is bool))
                {
                    result = false;
                }

            }

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            var result = new List<object>();
            result.Add(false);
            result.Add(false);

            return result.ToArray();
        }
    }
}
