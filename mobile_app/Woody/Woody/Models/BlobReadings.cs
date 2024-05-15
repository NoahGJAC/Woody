using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Enums;

namespace Woody.Models
{
    public class BlobReadings
    {        
        /// <summary>
        /// Gets or sets the value of the sensor reading.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the body of the sensor reading.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the timestamp of the sensor reading.
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the unit of the sensor reading.
        /// </summary>
        public ReadingUnit Unit { get; set; }

        /// <summary>
        /// Gets or sets the type of the sensor reading.
        /// </summary>
        public ReadingType ReadingType { get; set; }
    }
}
