using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Interfaces;

namespace Woody.Models
{
    public class PlantController : INotifyPropertyChanged, IController
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<ISensor> Sensors { get; private set; }
        public List<IActuator> Actuators { get; private set; }

        /// <summary>
        /// Reads the sensors and returns a list of readings.
        /// </summary>
        /// <returns>A list of sensor readings.</returns>
        /// <remarks>
        /// This method is not implemented and should be overridden in derived classes.
        /// </remarks>
        public List<IReading> ReadSensors()
        {
            throw new NotImplementedException();
        }

        public void ControlActuators(List<ICommand> commands)
        {
            throw new NotImplementedException();
        }
    }
}
