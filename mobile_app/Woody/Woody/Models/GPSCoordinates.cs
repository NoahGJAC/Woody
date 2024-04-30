using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Interfaces;

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
