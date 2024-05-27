using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Converters
{
    /// <summary>
    /// Converts a boolean value to a string representation of "ON" or "OFF" and vice versa.
    /// </summary>
    /// <remarks>
    /// This converter is useful for scenarios where a boolean value needs to be represented as a string,
    /// such as in UI elements that require text input, indicating an on/off state.
    /// </remarks>
    public class BoolToOnOffConverter : IValueConverter
    {

        /// <summary>
        /// Converts a boolean value to a string representation of "ON" or "OFF".
        /// </summary>
        /// <param name="value">The boolean value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns "ON" if the value is true; otherwise, "OFF".</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "ON" : "OFF";
        }

        /// <summary>
        /// Converts a string representation of "ON" or "OFF" back to a boolean value.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns true if the value is "ON"; otherwise, false.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().ToUpper() == "ON";
        }
    }
}
