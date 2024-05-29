using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

        private ObservableCollection<IReading<double>> _temperatureLevels;
        private ObservableCollection<IReading<double>> _humidityLevels;
        private ObservableCollection<IReading<double>> _soilMoistureLevels;

        /// <summary>
        /// Gets or sets the temperature level readings.
        /// </summary>
        public ObservableCollection<IReading<double>> TemperatureLevels
        {
            get => _temperatureLevels;
            set
            {
                if (_temperatureLevels != value)
                {
                    if (_temperatureLevels != null)
                    {
                        _temperatureLevels.CollectionChanged -= OnCollectionChanged;
                    }
                    _temperatureLevels = value;
                    if (_temperatureLevels != null)
                    {
                        _temperatureLevels.CollectionChanged += OnCollectionChanged;
                    }
                    OnPropertyChanged(nameof(TemperatureLevels));
                }
            }
        }

        /// <summary>
        /// Gets or sets the humidity level readings.
        /// </summary>
        public ObservableCollection<IReading<double>> HumidityLevels
        {
            get => _humidityLevels;
            set
            {
                if (_humidityLevels != value)
                {
                    if (_humidityLevels != null)
                    {
                        _humidityLevels.CollectionChanged -= OnCollectionChanged;
                    }
                    _humidityLevels = value;
                    if (_humidityLevels != null)
                    {
                        _humidityLevels.CollectionChanged += OnCollectionChanged;
                    }
                    OnPropertyChanged(nameof(HumidityLevels));
                }
            }
        }

        /// <summary>
        /// Gets or sets the soil moisture readings.
        /// </summary>
        public ObservableCollection<IReading<double>> SoilMoistureLevels
        {
            get => _soilMoistureLevels;
            set
            {
                if (_soilMoistureLevels != value)
                {
                    if (_soilMoistureLevels != null)
                    {
                        _soilMoistureLevels.CollectionChanged -= OnCollectionChanged;
                    }
                    _soilMoistureLevels = value;
                    if (_soilMoistureLevels != null)
                    {
                        _soilMoistureLevels.CollectionChanged += OnCollectionChanged;
                    }
                    OnPropertyChanged(nameof(SoilMoistureLevels));
                }
            }
        }

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
            get { return TemperatureLevels.Last(); }
        }

        /// <summary>
        /// Gets the average temperature.
        /// </summary>
        public double AverageTemperature
        {
            get { return TemperatureLevels.Average(x => x.Value); }
        }

        /// <summary>
        /// Gets the current humidity.
        /// </summary>
        public IReading<double> CurrentHumidity
        {
            get { return HumidityLevels.Last(); }
        }

        /// <summary>
        /// Gets the current soil moisture level.
        /// </summary>
        public IReading<double> CurrentSoilMoisture
        {
            get { return SoilMoistureLevels.Last(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlantRepo"/> class.
        /// </summary>
        public PlantRepo()
        {
            TemperatureLevels = new ObservableCollection<IReading<double>>();
            SoilMoistureLevels = new ObservableCollection<IReading<double>>();
            HumidityLevels = new ObservableCollection<IReading<double>>();
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(sender == TemperatureLevels ? nameof(TemperatureLevels) :
                            sender == HumidityLevels ? nameof(HumidityLevels) :
                            nameof(SoilMoistureLevels));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
