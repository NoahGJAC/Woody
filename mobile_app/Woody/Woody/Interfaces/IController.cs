using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Interfaces
{
    public interface IController<T>
    {
        public List<ISensor<T>> Sensors { get; }
        public List<IActuator<T>> Actuators { get; }

        public List<IReading<T>> ReadSesnsors();
        public void ControlActuators(List<ICommand<T>> commands);
    }
}
