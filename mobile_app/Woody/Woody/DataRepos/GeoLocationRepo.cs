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
        /// Gets or sets the geolocation controller.
        /// </summary>
        public GeoLocationController GeoLocationController { get; set; }

        private ContainerDatabaseService<GeoLocationController> geoDb;

        /// <summary>
        /// Gets the geolocation database service.
        /// </summary>
        public ContainerDatabaseService<GeoLocationController> GeoDb
        {
            get { return geoDb; }
        }


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
            geoDb = new ContainerDatabaseService<GeoLocationController>();

            //for testing purposes
            AddTestData();
        }

        /// <summary>
        /// Adds test data for geolocation properties.
        /// </summary>
        private void AddTestData()
        {
            Random random = new Random();

            var coordinates = new GPSCoordinates()
            {
                Latitude = new SensorReading<double>((random.NextDouble()*(90-(-90)))+(-90), DateTime.Today, ReadingUnit.DEGREES, ReadingType.LATITUDE),
                Longitude = new SensorReading<double>((random.NextDouble() * (180 - (-180))) + (-180), DateTime.Today, ReadingUnit.DEGREES, ReadingType.LONGITUDE),
                Altitude = new SensorReading<double>(4, DateTime.Today, ReadingUnit.DEGREES, ReadingType.ALTITUDE)
            };

            Pitch = new SensorReading<double>(random.Next(-90,90), DateTime.Today,ReadingUnit.DEGREES,ReadingType.PITCH);
            Roll = new SensorReading<double>(random.Next(-90, 90), DateTime.Today,ReadingUnit.DEGREES,ReadingType.ROLL);
            BuzzerState = new SensorReading<bool>(random.Next(0, 2) == 0, DateTime.Today,ReadingUnit.UNITLESS,ReadingType.BUZZER);
            Vibration = new SensorReading<bool>(random.Next(0, 2) == 0, DateTime.Today,ReadingUnit.UNITLESS,ReadingType.VIBRATION);
            GPS = new SensorReading<GPSCoordinates>(coordinates, DateTime.Today,ReadingUnit.UNITLESS,ReadingType.GPS);
            
        }
    }
}
