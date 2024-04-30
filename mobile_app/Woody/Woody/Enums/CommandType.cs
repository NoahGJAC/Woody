using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Enums
{
    /// <summary>
    /// Represents the types of commands that can be issued.
    /// </summary>
    public enum CommandType
    {
        /// <summary>
        /// Represents a command to control a buzzer.
        /// </summary>
        BUZZER,

        /// <summary>
        /// Represents a command to control temperature settings.
        /// </summary>
        TEMPERATURE,

        /// <summary>
        /// Represents a command to control humidity settings.
        /// </summary>
        HUMIDITY,

        /// <summary>
        /// Represents a command to control GPS settings.
        /// </summary>
        GPS
    }
}
