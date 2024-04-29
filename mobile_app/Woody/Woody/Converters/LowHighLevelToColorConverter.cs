using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Converters
{
    public class LowHighLevelToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Application.Current.Resources.TryGetValue("LightAllGoodGreen", out object colorResource);
            Color LightAllGoodGreen = (Color)colorResource;

            Application.Current.Resources.TryGetValue("LightWarningYellow", out object colorResource2);
            Color LightWarningYellow = (Color)colorResource2;

            if (value == null)
                return LightAllGoodGreen;

            string sValue = value as string;
            if (sValue == "Low")
                return LightAllGoodGreen;


            return LightWarningYellow;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
