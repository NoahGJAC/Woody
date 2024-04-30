using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Converters
{

    /// <summary>
    /// Converts a true/false level string value to a corresponding color resource and vice versa.
    /// </summary>
    /// <remarks>
    /// This converter is useful for scenarios where a string value indicating a true/false level (e.g., "True" or "False")
    /// needs to be represented as a color in UI elements, such as indicators for status levels.
    /// </remarks>
    public class TrueFalseLevelToColorConverter : IValueConverter
    {

        /// <summary>
        /// Converts a true/false level string value to a corresponding color resource.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns a color resource based on the input value. If the value is "True", returns a good color; otherwise, returns a warning color. If the value is null, returns a good color.</returns>
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

        /// <summary>
        /// Converts a color resource back to a true/false level string value.
        /// </summary>
        /// <param name="value">The color resource to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Not implemented.</returns>
        /// <exception cref="NotImplementedException">Thrown when the method is called.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
