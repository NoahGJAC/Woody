using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Enums;

namespace Woody.Converters
{
    internal class UserTypeToStringController : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is UserType ownerType)
            {
                return ownerType == UserType.OWNER ? " Fleet Owner" : " Fleet Farmer";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
