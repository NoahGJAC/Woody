using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Interfaces;

namespace Woody.Models
{
    public class Container
    {
        public string ContainerUuid { get; private set; }
        public string ContainerName { get; private set; }
        public List<IController> SubSystems { get; private set; }
    }
}
