using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Converters
{
    /// <summary>
    /// Converts a boolean value to a string representation of "OPEN" or "CLOSED" and vice versa.
    /// </summary>
    public class BoolToOpenClosedConverter : IValueConverter
    {

        /// <summary>
        /// Converts a boolean value to a string representation of "OPEN" or "CLOSED".
        /// </summary>
        /// <param name="value">The boolean value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns "OPEN" if the value is true; otherwise, "CLOSED".</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "OPEN" : "CLOSED";
        }

        /// <summary>
        /// Converts a string representation of "OPEN" or "CLOSED" back to a boolean value.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns true if the value is "OPEN"; otherwise, false.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().ToUpper() == "OPEN";
        }
    }
}
