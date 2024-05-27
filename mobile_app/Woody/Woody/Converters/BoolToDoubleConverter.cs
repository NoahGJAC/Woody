using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Converters
{
    /// <summary>
    /// Converts a boolean value to a double value and vice versa.
    /// </summary>
    public class BoolToDoubleConverter : IValueConverter
    {

        /// <summary>
        /// Converts a boolean value to a double value.
        /// </summary>
        /// <param name="value">The boolean value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns 1.0 if the value is true; otherwise, 0.0.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1.0 : 0.0;
        }

        /// <summary>
        /// Converts a double value back to a boolean value.
        /// </summary>
        /// <param name="value">The double value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns true if the value is greater than 0.5; otherwise, false.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value > 0.5;
        }
    }
}
