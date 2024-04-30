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
    /// Represents a security controller that manages sensors and actuators.
    /// </summary>
    public class SecurityController : INotifyPropertyChanged, IController
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the list of sensors managed by the security controller.
        /// </summary>
        public List<ISensor> Sensors { get; private set; }

        /// <summary>
        /// Gets the list of actuators managed by the security controller.
        /// </summary>
        public List<IActuator> Actuators { get; private set; }

        /// <summary>
        /// Controls the actuators based on the provided commands.
        /// </summary>
        /// <param name="commands">The list of commands to control the actuators.</param>
        /// <remarks>
        /// This method is currently empty and may need to be implemented in the future.
        /// </remarks>
        public void ControlActuators(List<ICommand> commands)
        {

        }

        /// <summary>
        /// Reads the sensors and returns a list of readings.
        /// </summary>
        /// <returns>A list of sensor readings.</returns>
        /// <exception cref="NotImplementedException">Thrown when the method is not implemented.</exception>
        public List<IReading> ReadSensors()
        {
            throw new NotImplementedException();
        }
    }
}
