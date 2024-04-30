using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Enums;

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
