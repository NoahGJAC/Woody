using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Enums;
using Woody.Interfaces;

namespace Woody.Models
{
    public class SensorReading<T> : IReading<T>
    {
        public T Value { get; set; }
        public DateTime TimeStamp { get; set; }
        public ReadingUnit Unit { get; set; }
        public ReadingType ReadingType { get; set; }

        public SensorReading(T value, DateTime timeStamp, ReadingUnit unit, ReadingType readingType)
        {
            Value = value;
            TimeStamp = timeStamp;
            Unit = unit;
            ReadingType = readingType;
        }
    }
    
}
