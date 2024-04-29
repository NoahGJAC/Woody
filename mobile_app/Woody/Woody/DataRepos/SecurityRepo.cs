using System.ComponentModel;
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
        public IReading<int> LuminosityCurrent {  get; set; }
        public IReading<float> NoiseCurrent {  get; set; }
        public IReading<bool> DoorState { get; set; }
        public IReading<bool> MotionState { get; set; }
        public IReading<bool> BuzzerState { get; set; }
        public IReading<bool> LockState { get; set; }
        

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

            DoorState = new SensorReading<bool>(random.Next(0, 2) == 0, DateTime.Now, ReadingUnit.UNITLESS, ReadingType.DOOR);
            MotionState = new SensorReading<bool>(random.Next(0, 2) == 0, DateTime.Now, ReadingUnit.UNITLESS, ReadingType.MOTION);
            BuzzerState = new SensorReading<bool>(random.Next(0, 2) == 0, DateTime.Now, ReadingUnit.UNITLESS, ReadingType.BUZZER);
            LockState = new SensorReading<bool>(random.Next(0, 2) == 0, DateTime.Now, ReadingUnit.UNITLESS, ReadingType.DOOR_LOCK);
            LuminosityCurrent = new SensorReading<int>(random.Next(0, 1000),DateTime.Now, ReadingUnit.LUX, ReadingType.LUMINOSITY);
            NoiseCurrent = new SensorReading<float>((float)random.NextDouble() * 100, DateTime.Now, ReadingUnit.LOUDNESS, ReadingType.LOUDNESS);
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
