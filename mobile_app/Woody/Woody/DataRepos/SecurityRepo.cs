﻿using System.ComponentModel;
using Woody.Enums;
using Woody.Interfaces;
using Woody.Models;
using Woody.Services;

namespace Woody.DataRepos
{
    public class SecurityRepo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public SecurityController SecurityController { get; set; }

        private ContainerDatabaseService<SecurityController> securityDb;

        public ContainerDatabaseService<SecurityController> SecurityDb
        {
            get { return securityDb; }
        }
        public List<IReading<float>> NoiseLevels{ get; set; }

        public List<IReading<int>> LuminosityLevels { get; set; }


        public SecurityRepo()
        {
            securityDb = new ContainerDatabaseService<SecurityController>();

            //for testing purposes
            AddTestData();
        }

        private void AddTestData(int sample_points = 40)
        {
            NoiseLevels = new List<IReading<float>>();
            LuminosityLevels = new List<IReading<int>>();

            Random random = new Random();
            DateTime day;

            for (int i = 0; i < sample_points; i++)
            {
                day = DateTime.Now.AddDays(-i);

                // Generate random noise level
                var noiseValue = (float)(random.NextDouble() * 100);
                var noiseReading = new SensorReading<float>(noiseValue, day, ReadingUnit.LOUDNESS, ReadingType.LOUDNESS);
                NoiseLevels.Add(noiseReading);

                // Generate random luminosity level
                var luminosityValue = random.Next(0, 100000);
                var luminosityReading = new SensorReading<int>(luminosityValue, day, ReadingUnit.LUX, ReadingType.LUMINOSITY);
                LuminosityLevels.Add(luminosityReading);
            }
        }

    }
}