using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Interfaces;

namespace Woody.Models
{
    /// <summary>
    /// Represents a container that holds a collection of subsystems.
    /// </summary>
    public class Container
    {
        /// <summary>
        /// Gets the unique identifier of the container.
        /// </summary>
        public string ContainerUuid { get; private set; }

        /// <summary>
        /// Gets the name of the container.
        /// </summary>
        public string ContainerName { get; private set; }

        /// <summary>
        /// Gets the list of subsystems contained within the container.
        /// </summary>
        public List<IController> SubSystems { get; private set; }
    }
}
