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

namespace Woody.DataRepos
{
    public class GeoLocationRepo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public GeoLocationController GeoLocationController { get; set; }

        private ContainerDatabaseService<GeoLocationController> securityDb;

        public ContainerDatabaseService<GeoLocationController> SecurityDb
        {
            get { return securityDb; }
        }
        public IReading<double> Pitch { get; set; }
        public IReading<double> Roll { get; set; }
        public IReading<bool> BuzzerState { get; set; }
        public IReading<GPSCoordinates> GPS { get; set; }

        public GeoLocationRepo()
        {
            securityDb = new ContainerDatabaseService<GeoLocationController>();

            //for testing purposes
            AddTestData();
        }
        private void AddTestData()
        {
            Random random = new Random();

            var coordinates = new GPSCoordinates()
            {
                Latitude = new SensorReading<double>(53.24586357201233, DateTime.Today, ReadingUnit.DEGREES, ReadingType.LATITUDE),
                Longitude = new SensorReading<double>(-1.6171050737883514, DateTime.Today, ReadingUnit.DEGREES, ReadingType.LONGITUDE),
                Altitude = new SensorReading<double>(4, DateTime.Today, ReadingUnit.DEGREES, ReadingType.ALTITUDE)
            };

            Pitch = new SensorReading<double>(random.Next(-90,90), DateTime.Today,ReadingUnit.DEGREES,ReadingType.PITCH);
            Roll = new SensorReading<double>(random.Next(-90, 90), DateTime.Today,ReadingUnit.DEGREES,ReadingType.ROLL);
            BuzzerState = new SensorReading<bool>(random.Next(0, 2) == 0, DateTime.Today,ReadingUnit.UNITLESS,ReadingType.BUZZER);
            GPS = new SensorReading<GPSCoordinates>(coordinates, DateTime.Today,ReadingUnit.UNITLESS,ReadingType.GPS);
            
        }
    }
}
