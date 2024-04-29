using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Converters
{

    /// <summary>
    /// Converts a boolean value to a string representation of "LOCK" or "UNLOCK" and vice versa.
    /// </summary>
    public class BoolToLockUnlockConverter : IValueConverter
    {
        /// <summary>
        /// Converts a boolean value to a string representation of "LOCK" or "UNLOCK".
        /// </summary>
        /// <param name="value">The boolean value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns "LOCK" if the value is true; otherwise, "UNLOCK".</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "LOCK" : "UNLOCK";
        }

        /// <summary>
        /// Converts a string representation of "LOCK" or "UNLOCK" back to a boolean value.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns true if the value is "LOCK"; otherwise, false.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString().ToUpper() == "LOCK";
        }
    }
}
