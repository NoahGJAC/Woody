using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Converters
{

    /// <summary>
    /// Converts a boolean value to a string representation of "VIBRATION DETECTED" or "NO VIBRATION DETECTED" and vice versa.
    /// </summary>
    /// <remarks>
    /// This converter is useful for scenarios where a boolean value needs to be represented as a string,
    /// such as in UI elements that require text input, indicating vibration detection status.
    /// </remarks>
    public class BoolToVibrationConverter : IValueConverter
    {
        /// <summary>
        /// Converts a boolean value to a string representation of "VIBRATION DETECTED" or "NO VIBRATION DETECTED".
        /// </summary>
        /// <param name="value">The boolean value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns "VIBRATION DETECTED" if the value is true; otherwise, "NO VIBRATION DETECTED". If the value is not a boolean, returns "Unknown".</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool booleanValue)
            {
                return booleanValue ? "True" : "False";
            }
            return "Unknown";
        }

        /// <summary>
        /// Converts a string representation of "VIBRATION DETECTED" or "NO VIBRATION DETECTED" back to a boolean value.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns true if the value is "VIBRATION DETECTED"; otherwise, false.</returns>
        /// <exception cref="NotImplementedException">Thrown when the method is called.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
