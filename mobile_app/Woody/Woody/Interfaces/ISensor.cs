using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Interfaces
{
    public interface ISensor<T>
    {
        public string SensorID { get; set; }
        public string SensorModel { get; set; }

        public List<IReading<T>> ReadSensor();
    }
}
