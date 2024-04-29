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
