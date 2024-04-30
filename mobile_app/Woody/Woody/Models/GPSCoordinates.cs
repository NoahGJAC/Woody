using System;
using System.Collections.Generic;
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
    /// Represents GPS coordinates including longitude, latitude, and altitude.
    /// </summary>
    public class GPSCoordinates
    {
        /// <summary>
        /// Gets or sets the longitude reading.
        /// </summary>
        public IReading<double> Longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude reading.
        /// </summary>
        public IReading<double> Latitude { get; set; }

        /// <summary>
        /// Gets or sets the altitude reading.
        /// </summary>
        public IReading<double> Altitude { get; set; }
    }
}
