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
    /// Represents a basic interface for an actuator.
    /// </summary>
    public interface IActuator
    {
        /// <summary>
        /// Gets or sets the unique identifier of the actuator.
        /// </summary>
        public string ActuatorID { get; set; }

        /// <summary>
        /// Gets or sets the current state of the actuator.
        /// </summary>
        public string State { get; set; }
    }

    /// <summary>
    /// Represents a generic interface for an actuator with a specific type.
    /// </summary>
    /// <typeparam name="T">The type of the command that the actuator can control.</typeparam>
    public interface IActuator<T> : IActuator
    {
        /// <summary>
        /// Controls the actuator with the specified command.
        /// </summary>
        /// <param name="command">The command to control the actuator.</param>
        public void ControlActuator(ICommand<T> command);
    }
}
