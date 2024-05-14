using System.ComponentModel;
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
    /// Represents a repository for security data, including noise levels, luminosity levels, and various sensor states.
    /// </summary>
    public class SecurityRepo : INotifyPropertyChanged
    {

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the security controller.
        /// </summary>
        public SecurityController SecurityController { get; set; }

        private ContainerDatabaseService<SecurityController> securityDb;

        /// <summary>
        /// Gets the security database service.
        /// </summary>
        public ContainerDatabaseService<SecurityController> SecurityDb
        {
            get { return securityDb; }
        }

        /// <summary>
        /// Gets or sets the list of noise level readings.
        /// </summary>
        public List<IReading<float>> NoiseLevels { get; set; }

        /// <summary>
        /// Gets or sets the list of luminosity level readings.
        /// </summary>
        public List<IReading<int>> LuminosityLevels { get; set; }

        /// <summary>
        /// Gets the current luminosity reading.
        /// </summary>
        public IReading<int> LuminosityCurrent
        {
            get
            {
                return LuminosityLevels.Last();
            }
        }

        /// <summary>
        /// Gets the current noise reading.
        /// </summary>
        public IReading<float> NoiseCurrent
        {
            get
            {
                return NoiseLevels.Last();
            }
        }

        /// <summary>
        /// Gets or sets the door state reading.
        /// </summary>
        public IReading<bool> DoorState { get; set; }

        /// <summary>
        /// Gets or sets the motion state reading.
        /// </summary>
        public IReading<bool> MotionState { get; set; }

        /// <summary>
        /// Gets or sets the buzzer state reading.
        /// </summary>
        public IReading<bool> BuzzerState { get; set; }

        /// <summary>
        /// Gets or sets the lock state reading.
        /// </summary>
        public IReading<bool> LockState { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityRepo"/> class.
        /// </summary>
        public SecurityRepo()
        {
            securityDb = new ContainerDatabaseService<SecurityController>();
            NoiseLevels = new List<IReading<float>>();
            LuminosityLevels = new List<IReading<int>>();

            //for testing purposes
            AddTestData();
        }

        /// <summary>
        /// Adds test data for security properties.
        /// </summary>
        /// <param name="sample_points">The number of sample points to generate for noise and luminosity levels.</param>
        private void AddTestData(int sample_points = 40)
        {
            //NoiseLevels = new List<IReading<float>>();
            //LuminosityLevels = new List<IReading<int>>();

            Random random = new Random();

            //DoorState = new SensorReading<bool>(random.Next(0, 2) == 0, DateTime.Now, ReadingUnit.UNITLESS, ReadingType.DOOR);
            //MotionState = new SensorReading<bool>(random.Next(0, 2) == 0, DateTime.Now, ReadingUnit.UNITLESS, ReadingType.MOTION);
            //BuzzerState = new SensorReading<bool>(random.Next(0, 2) == 0, DateTime.Now, ReadingUnit.UNITLESS, ReadingType.BUZZER);
            //LockState = new SensorReading<bool>(random.Next(0, 2) == 0, DateTime.Now, ReadingUnit.UNITLESS, ReadingType.DOOR_LOCK);

            DateTime day;

            for (int i = 0; i < sample_points; i++)
            {
                day = DateTime.Now.AddDays(-i);

                // Generate random noise level
                var noiseValue = (float)(random.NextDouble() * 100);
                var noiseReading = new SensorReading<float>(noiseValue, day, ReadingUnit.LOUDNESS, ReadingType.LOUDNESS);
                NoiseLevels.Add(noiseReading);

                // Generate random luminosity level
                //var luminosityValue = random.Next(0, 100000);
                //var luminosityReading = new SensorReading<int>(luminosityValue, day, ReadingUnit.LUX, ReadingType.LUMINOSITY);
                //LuminosityLevels.Add(luminosityReading);
            }
        }

    }
}
