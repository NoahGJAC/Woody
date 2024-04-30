using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Converters
{

    /// <summary>
    /// Converts a boolean value to a string representation of "MOTION" or "NO MOTION" and vice versa.
    /// </summary>
    public class BoolToMotionDetectedConverter : IValueConverter
    {

        /// <summary>
        /// Converts a boolean value to a string representation of "MOTION" or "NO MOTION".
        /// </summary>
        /// <param name="value">The boolean value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns "MOTION" if the value is true; otherwise, "NO MOTION".</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "MOTION" : "NO MOTION";
        }

        /// <summary>
        /// Converts a string representation of "MOTION" or "NO MOTION" back to a boolean value.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns true if the value is "MOTION"; otherwise, false.</returns>
        public object ConvertBack(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        )
        {
            return value.ToString().ToUpper() == "MOTION DETECTED";
        }
    }
}
