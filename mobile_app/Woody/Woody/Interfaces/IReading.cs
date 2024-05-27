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
    /// Represents a basic interface for a reading.
    /// </summary>
    public interface IReading
    {
        /// <summary>
        /// Gets or sets the unit of the reading.
        /// </summary>
        ReadingUnit Unit { get; set; }

        /// <summary>
        /// Gets or sets the type of the reading.
        /// </summary>
        ReadingType ReadingType { get; set; }

        /// <summary>
        /// Gets or sets the command associated with the reading.
        /// </summary>
        Models.Command<string> Command { get; set; }
    }

    /// <summary>
    /// Represents a generic interface for a reading with a specific type.
    /// </summary>
    /// <typeparam name="T">The type of the value that the reading carries.</typeparam>
    public interface IReading<T> : IReading
    {
        /// <summary>
        /// Gets or sets the value of the reading.
        /// </summary>
        T Value { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the reading.
        /// </summary>
        DateTime TimeStamp { get; set; }
    }
}