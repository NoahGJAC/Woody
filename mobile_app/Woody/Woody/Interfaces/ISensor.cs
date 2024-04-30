using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Interfaces
{
    /// <summary>
    /// Represents a basic interface for a sensor.
    /// </summary>
    public interface ISensor
    {
        /// <summary>
        /// Gets or sets the unique identifier of the sensor.
        /// </summary>
        public string SensorID { get; set; }

        /// <summary>
        /// Gets or sets the model of the sensor.
        /// </summary>
        public string SensorModel { get; set; }
    }

    /// <summary>
    /// Represents a generic interface for a sensor with a specific type.
    /// </summary>
    /// <typeparam name="T">The type of the value that the sensor reads.</typeparam>
    public interface ISensor<T> : ISensor
    {
        /// <summary>
        /// Reads the sensor and returns a list of readings.
        /// </summary>
        /// <returns>A list of sensor readings.</returns>
        public List<IReading<T>> ReadSensor();
    }
}
