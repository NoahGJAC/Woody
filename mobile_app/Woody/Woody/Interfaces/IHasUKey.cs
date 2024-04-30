using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Interfaces
{
    /// <summary>
    /// Represents an interface for objects that have a unique key.
    /// </summary>
    public interface IHasUKey
    {
        /// <summary>
        /// Gets or sets the unique key of the object.
        /// </summary>
        public string Key { get; set; }
    }
}
