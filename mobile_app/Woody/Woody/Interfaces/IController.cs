using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */
namespace Woody.Interfaces
{
    /// <summary>
    /// Represents a controller that manages sensors and actuators.
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Gets the list of sensors managed by the controller.
        /// </summary>
        List<ISensor> Sensors { get; }

        /// <summary>
        /// Gets the list of actuators managed by the controller.
        /// </summary>
        List<IActuator> Actuators { get; }

        /// <summary>
        /// Reads the sensors and returns a list of readings.
        /// </summary>
        /// <returns>A list of sensor readings.</returns>
        List<IReading> ReadSensors();

        /// <summary>
        /// Controls the actuators based on the provided commands.
        /// </summary>
        /// <param name="commands">The list of commands to control the actuators.</param>
        void ControlActuators(List<ICommand> commands);
    }
}
