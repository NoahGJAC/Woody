using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

        private ObservableCollection<IReading<float>> _noiseLevels;
        private ObservableCollection<IReading<int>> _luminosityLevels;
        /// <summary>
        /// Gets or sets the list of noise level readings.
        /// </summary>
        public ObservableCollection<IReading<float>> NoiseLevels
        {
            get => _noiseLevels;
            set
            {
                if (_noiseLevels != value)
                {
                    if (_noiseLevels != null)
                    {
                        _noiseLevels.CollectionChanged -= OnCollectionChanged;
                    }
                    _noiseLevels = value;
                    if (_noiseLevels != null)
                    {
                        _noiseLevels.CollectionChanged += OnCollectionChanged;
                    }
                    OnPropertyChanged(nameof(NoiseLevels));
                }
            }
        }

        /// <summary>
        /// Gets or sets the list of luminosity level readings.
        /// </summary>
        public ObservableCollection<IReading<int>> LuminosityLevels
        {
            get => _luminosityLevels;
            set
            {
                if (_luminosityLevels != value)
                {
                    if (_luminosityLevels != null)
                    {
                        _luminosityLevels.CollectionChanged -= OnCollectionChanged;
                    }
                    _luminosityLevels = value;
                    if (_luminosityLevels != null)
                    {
                        _luminosityLevels.CollectionChanged += OnCollectionChanged;
                    }
                    OnPropertyChanged(nameof(LuminosityLevels));
                }
            }
        }

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
            NoiseLevels = new ObservableCollection<IReading<float>>();
            LuminosityLevels = new ObservableCollection<IReading<int>>();
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(sender == NoiseLevels ? nameof(NoiseLevels) : nameof(LuminosityLevels));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
