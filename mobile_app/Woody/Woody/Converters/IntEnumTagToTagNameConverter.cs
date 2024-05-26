using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Enums;

namespace Woody.Converters
{
    public class IntEnumTagToTagNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var taskType = (TaskType)value;
            return taskType.ToString().ToLower();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum.Parse(targetType, value.ToString(), true);
        }
    }
}
