using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Converters
{
    /// <summary>
    /// Converts a user type value to a boolean indicating whether it matches a specified parameter, and vice versa.
    /// </summary>
    public class UserTypeToRadioButtonConverter : IValueConverter
    {

        /// <summary>
        /// Converts a user type value to a boolean indicating whether it matches a specified parameter.
        /// </summary>
        /// <param name="value">The user type value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The parameter to compare against the user type value.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns true if the user type value equals the specified parameter; otherwise, false.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(parameter);
        }

        /// <summary>
        /// Converts a boolean value back to the specified parameter if true, otherwise does nothing.
        /// </summary>
        /// <param name="value">The boolean value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The parameter to return if the boolean value is true.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>Returns the specified parameter if the boolean value is true; otherwise, Binding.DoNothing.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
