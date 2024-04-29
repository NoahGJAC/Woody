using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Converters
{
    public class TrueFalseLevelToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<string> GoodColorNames = new List<string>{"AllGoodGreen", "MediumAllGoodGreen", "LightAllGoodGreen", "LimeGreen",
            "ZestyGreen" };
            List<string> WarningColorNames = new List<string> { "WarningYellow", "MediumWarningYellow", "LightWarningYellow" };
            Random random = new Random();

            if (value == null || value as string == "False")
            {
                //get random good color from array
                Application.Current.Resources.TryGetValue(GoodColorNames[random.Next(0, GoodColorNames.Count)], out object colorResource);
                Color GoodColor = (Color)colorResource;
                return GoodColor;
            }

            Application.Current.Resources.TryGetValue(WarningColorNames[random.Next(0, WarningColorNames.Count)], out object colorResource2);
            Color WarningColor = (Color)colorResource2;
            return WarningColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
