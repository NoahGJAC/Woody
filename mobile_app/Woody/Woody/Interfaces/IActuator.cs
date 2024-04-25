using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Interfaces
{
    public interface IActuator<T>
    {
        public string ActuatorID { get; set; }
        public string State { get; set; }

        public void ControlActuator(ICommand<T> command);
    }
}
