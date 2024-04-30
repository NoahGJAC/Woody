using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Represents a plant controller that manages sensors and actuators.
    /// </summary>
    public class PlantController : INotifyPropertyChanged, IController
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the list of sensors managed by the plant controller.
        /// </summary>
        public List<ISensor> Sensors { get; private set; }

        /// <summary>
        /// Gets the list of actuators managed by the plant controller.
        /// </summary>
        public List<IActuator> Actuators { get; private set; }

        /// <summary>
        /// Reads the sensors and returns a list of readings.
        /// </summary>
        /// <returns>A list of sensor readings.</returns>
        /// <exception cref="NotImplementedException">Thrown when called.</exception>
        public List<IReading> ReadSensors()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Controls the actuators based on the provided commands.
        /// </summary>
        /// <param name="commands">The list of commands to control the actuators.</param>
        /// <exception cref="NotImplementedException">Thrown when called.</exception>
        public void ControlActuators(List<ICommand> commands)
        {
            throw new NotImplementedException();
        }
    }
}
