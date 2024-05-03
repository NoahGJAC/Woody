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
    /// Represents a repository for plant data, including temperature, humidity, soil moisture levels, water level, fan and light state.
    /// </summary>
    public class PlantRepo : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private ContainerDatabaseService<PlantController> plantDb;

        /// <summary>
        /// Gets the plant database service.
        /// </summary>
        public ContainerDatabaseService<PlantController> PlantDb
        {
            get { return plantDb; }
        }

        /// <summary>
        /// Gets or sets the temperature level readings.
        /// </summary>
        public List<IReading<double>> TemperatureLevels { get; set; }

        /// <summary>
        /// Gets or sets the humidity level readings.
        /// </summary>
        public List<IReading<double>> HumidityLevels { get; set; }

        /// <summary>
        /// Gets or sets the soil moisture readings.
        /// </summary>
        public List<IReading<double>> SoilMoistureLevels { get; set; }

        /// <summary>
        /// Gets or sets the water level reading.
        /// </summary>
        public IReading<int> WaterLevel { get; set; }

        /// <summary>
        /// Gets or sets the fan state reading.
        /// </summary>
        public IReading<bool> FanState { get; set; }

        /// <summary>
        /// Gets or sets the light state reading.
        /// </summary>
        public IReading<bool> LightState { get; set; }

        /// <summary>
        /// Gets the last temperature recorded.
        /// </summary>
        public IReading<double> CurrentTemperature
        {
            get
            {
                return TemperatureLevels.Last();
            }
        }

        /// <summary>
        /// Gets the average temperature.
        /// </summary>
        public double AverageTemperature
        {
            get
            {
                return TemperatureLevels.Average(x => x.Value);
            }
        }

        /// <summary>
        /// Gets the current humidity.
        /// </summary>
        public IReading<double> CurrentHumidity
        {
            get
            {
                return HumidityLevels.Last();
            }
        }

        /// <summary>
        /// Gets the current soil moisture level.
        /// </summary>
        public IReading<double> CurrentSoilMoisture
        {
            get
            {
                return SoilMoistureLevels.Last();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlantRepo"/> class.
        /// </summary>
        public PlantRepo()
        {
            plantDb = new ContainerDatabaseService<PlantController>();
            AddTestData();
        }

        private void AddTestData(int sample_points = 40)
        {
            TemperatureLevels = new List<IReading<double>>();
            SoilMoistureLevels = new List<IReading<double>>();
            HumidityLevels = new List<IReading<double>>();

            Random random = new Random();

            WaterLevel = new SensorReading<int>(
                random.Next(0, 100),
                DateTime.Now,
                ReadingUnit.PERCENTAGE,
                ReadingType.WATER_LEVEL
            );

            FanState = new SensorReading<bool>(
                random.Next(0, 2) == 0,
                DateTime.Now,
                ReadingUnit.UNITLESS,
                ReadingType.FAN
            );
            LightState = new SensorReading<bool>(
                random.Next(0, 2) == 0,
                DateTime.Now,
                ReadingUnit.UNITLESS,
                ReadingType.LIGHT
            );

            DateTime day;

            for (int i = 0; i < sample_points; i++)
            {
                day = DateTime.Now.AddDays(-i);

                // Generate random temperature reading
                TemperatureLevels.Add(
                   new SensorReading<double>(
                        Math.Round(random.NextDouble() * 40, 2),
                        day,
                        ReadingUnit.CELCIUS,
                        ReadingType.TEMPERATURE
                    )
                );

                // Generate random soil moisture reading
                SoilMoistureLevels.Add(
                    new SensorReading<double>(
                        Math.Round(random.NextDouble() * 2000, 2),
                        day,
                        ReadingUnit.UNITLESS,
                        ReadingType.SOIL_MOISTURE
                    )
                );

                // Generate random humidity reading
                HumidityLevels.Add(
                    new SensorReading<double>(
                        Math.Round(random.NextDouble() * 100, 2),
                        day,
                        ReadingUnit.PERCENTAGE,
                        ReadingType.HUMIDITY
                    )
                );
            }
        }
    }
}
