using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Interfaces
{
    public interface IController
    {
        public List<ISensor> Sensors { get; }
        public List<IActuator> Actuators { get; }

        public List<IReading> ReadSensors();
        public void ControlActuators(List<ICommand> commands);
    }
}
