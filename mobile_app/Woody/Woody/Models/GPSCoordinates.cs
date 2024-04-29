using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Interfaces;

namespace Woody.Models
{
    public class GPSCoordinates
    {
       public IReading<double> Longitude { get; set; }
       public IReading<double> Latitude { get; set; }
       public IReading<double> Altitude { get; set; }
    }
}
