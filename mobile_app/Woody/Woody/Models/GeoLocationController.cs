using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Interfaces;

namespace Woody.Models
{
    /// <summary>
    /// Represents a controller for managing geolocation-related sensors and actuators.
    /// </summary>
    public class GeoLocationController : INotifyPropertyChanged, IController
    {
        /// <summary>
        /// Gets the list of sensors managed by the controller.
        /// </summary>
        public List<ISensor> Sensors { get; private set; }

        /// <summary>
        /// Gets the list of actuators managed by the controller.
        /// </summary>
        public List<IActuator> Actuators { get; private set; }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Controls the actuators based on the provided commands.
        /// </summary>
        /// <param name="commands">The list of commands to control the actuators.</param>
        /// <remarks>
        /// This method is not implemented and should be overridden in derived classes.
        /// </remarks>
        public void ControlActuators(List<ICommand> commands)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads the sensors and returns a list of readings.
        /// </summary>
        /// <returns>A list of sensor readings.</returns>
        /// <remarks>
        /// This method is not implemented and should be overridden in derived classes.
        /// </remarks>
        public List<IReading> ReadSensors()
        {
            throw new NotImplementedException();
        }
    }
}
