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
    /// Represents a mock implementation of the <see cref="IController"/> interface for testing purposes.
    /// </summary>
    public class MockController : INotifyPropertyChanged, IController
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the list of sensors managed by the controller.
        /// </summary>
        public List<ISensor> Sensors { get; private set; }

        /// <summary>
        /// Gets the list of actuators managed by the controller.
        /// </summary>
        public List<IActuator> Actuators { get; private set; }

        /// <summary>
        /// Controls the actuators based on the provided commands.
        /// </summary>
        /// <param name="commands">The list of commands to control the actuators.</param>
        /// <remarks>
        /// This method is not implemented in the mock class and will throw a <see cref="NotImplementedException"/>.
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
        /// This method is not implemented in the mock class and will throw a <see cref="NotImplementedException"/>.
        /// </remarks>
        public List<IReading> ReadSensors()
        {
            throw new NotImplementedException();
        }
    }
}
