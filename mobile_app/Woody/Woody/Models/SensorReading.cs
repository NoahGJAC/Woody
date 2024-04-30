using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Enums;
using Woody.Interfaces;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */
namespace Woody.Models
{
    /// <summary>
    /// Represents a sensor reading with a specific type.
    /// </summary>
    /// <typeparam name="T">The type of the value that the sensor reading carries.</typeparam>
    public class SensorReading<T> : IReading<T>, INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the value of the sensor reading.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the sensor reading.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the unit of the sensor reading.
        /// </summary>
        public ReadingUnit Unit { get; set; }

        /// <summary>
        /// Gets or sets the type of the sensor reading.
        /// </summary>
        public ReadingType ReadingType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SensorReading{T}"/> class with the specified value, timestamp, unit, and reading type.
        /// </summary>
        /// <param name="value">The value of the sensor reading.</param>
        /// <param name="timeStamp">The timestamp of the sensor reading.</param>
        /// <param name="unit">The unit of the sensor reading.</param>
        /// <param name="readingType">The type of the sensor reading.</param>
        public SensorReading(T value, DateTime timeStamp, ReadingUnit unit, ReadingType readingType)
        {
            Value = value;
            TimeStamp = timeStamp;
            Unit = unit;
            ReadingType = readingType;
        }
    }
    
}
