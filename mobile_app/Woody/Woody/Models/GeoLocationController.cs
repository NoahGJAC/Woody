using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Interfaces;

namespace Woody.Models
{
    public class GeoLocationController : INotifyPropertyChanged, IController
    {
        public List<ISensor> Sensors { get; private set; }

        public List<IActuator> Actuators { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        // Don't think the models should have methods like this idk..
        public void ControlActuators(List<ICommand> commands)
        {
            throw new NotImplementedException();
        }

        public List<IReading> ReadSensors()
        {
            throw new NotImplementedException();
        }
    }
}
