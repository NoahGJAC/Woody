using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Enums;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */
namespace Woody.Interfaces
{

    /// <summary>
    /// Represents a basic interface for a command.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets or sets the type of the command.
        /// </summary>
        CommandType CommandType { get; set; }
        SubSystemType SubSystem { get; set; }

    }

    /// <summary>
    /// Represents a generic interface for a command with a specific type.
    /// </summary>
    /// <typeparam name="T">The type of the value that the command carries.</typeparam>
    public interface ICommand<T>: ICommand
    {
        /// <summary>
        /// Gets or sets the value of the command.
        /// </summary>
        T Value { get; set; }
    }
}
