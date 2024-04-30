using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Converters
{
    /// <summary>
    /// Converts a noise value to a string representation of "Low", "Medium", or "High" and vice versa.
    /// </summary>
    public class NoiseValueToLevelConverter : IValueConverter
    {

        /// <summary>
        /// Converts a noise value to a string representation of "Low", "Medium", or "High".
        /// </summary>
        /// <param name="value">The noise value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns "Low" if the value is less than 25, "High" if the value is greater than 75, and "Medium" otherwise.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float noise = (float)value;
            return noise < 25 ? "Low" : noise > 75 ? "High" : "Medium";
        }

        /// <summary>
        /// Converts a string representation of "Low", "Medium", or "High" back to a noise value.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
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
