using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Interfaces;

namespace Woody.Models
{
    public class SecurityController : INotifyPropertyChanged, IController
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public List<ISensor> Sensors { get; private set; }

        public List<IActuator> Actuators { get; private set; }

        // Don't think the models should have methods like this idk..
        public void ControlActuators(List<ICommand> commands)
        {
            
        }

        public List<IReading> ReadSensors()
        {
            throw new NotImplementedException();
        }
    }
}
