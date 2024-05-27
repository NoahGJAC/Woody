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
            NoiseLevels = new List<IReading<float>>();
            LuminosityLevels = new List<IReading<int>>();
        }
    }
}
