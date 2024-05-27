using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Enums;
using Woody.Interfaces;
using Woody.Models;
using Woody.Services;

/*
 * Team: Woody
 * Section 1
 * Winter 2024, 04/30/2024
 * 420-6A6 App Dev III
 */
namespace Woody.DataRepos
{
    /// <summary>
    /// Represents a repository for geolocation data, including GPS coordinates, pitch, roll, buzzer state, and vibration.
    /// </summary>
    public class GeoLocationRepo : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the pitch reading.
        /// </summary>
        public IReading<double> Pitch { get; set; }

        /// <summary>
        /// Gets or sets the roll reading.
        /// </summary>
        public IReading<double> Roll { get; set; }

        /// <summary>
        /// Gets or sets the buzzer state reading.
        /// </summary>
        public IReading<bool> BuzzerState { get; set; }

        /// <summary>
        /// Gets or sets the vibration reading.
        /// </summary>
        public IReading<bool> Vibration { get; set; }

        /// <summary>
        /// Gets or sets the GPS coordinates reading.
        /// </summary>
        public IReading<GPSCoordinates> GPS { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="GeoLocationRepo"/> class.
        /// </summary>
        public GeoLocationRepo()
        {
            GPS = new SensorReading<GPSCoordinates>();
            GPS.Value = new GPSCoordinates();
        }

    }
}
