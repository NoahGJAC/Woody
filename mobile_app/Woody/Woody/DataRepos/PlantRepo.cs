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
    public class PlantRepo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ContainerDatabaseService<PlantController> plantDb;

        public ContainerDatabaseService<PlantController> PlantDb
        {
            get { return plantDb; }
        }

        public List<IReading<double>> TemperatureLevels { get; set; }
        public List<IReading<double>> HumidityLevels { get; set; }
        public List<IReading<double>> SoilMoistureLevels { get; set; }
        public IReading<int> WaterLevel { get; set; }

        public IReading<bool> FanState { get; set; }
        public IReading<bool> LightState { get; set; }

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
                        random.NextDouble() * 40,
                        day,
                        ReadingUnit.CELCIUS,
                        ReadingType.TEMPERATURE
                    )
                );

                // Generate random soil moisture reading
                SoilMoistureLevels.Add(
                    new SensorReading<double>(
                        random.NextDouble() * 2000,
                        day,
                        ReadingUnit.UNITLESS,
                        ReadingType.SOIL_MOISTURE
                    )
                );

                // Generate random humidity reading
                HumidityLevels.Add(
                    new SensorReading<double>(
                        random.NextDouble() * 100,
                        day,
                        ReadingUnit.PERCENTAGE,
                        ReadingType.HUMIDITY
                    )
                );
            }
        }
    }
}
