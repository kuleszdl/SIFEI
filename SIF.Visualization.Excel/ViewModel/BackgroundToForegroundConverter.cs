using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace SIF.Visualization.Excel.ViewModel
{
    public class BackgroundToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SolidColorBrush brush = value as SolidColorBrush;

            if (brush == null)
                return null;

            var perceivedColor = this.PerceivedBrightness(brush.Color);

            if (perceivedColor < 140) return new SolidColorBrush(new Color() { A = 220, R = 255, G = 255, B = 255 });
            else return new SolidColorBrush(new Color() { A = 220, R = 0, G = 0, B = 0 });
        }

        private int PerceivedBrightness(Color c)
        {
            return (int)Math.Sqrt(c.R * c.R * .241 + c.G * c.G * .691 + c.B * c.B * .068);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
