using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Enums;

namespace Woody.Interfaces
{
    public interface IReading<T>
    {
        ReadingUnit Unit { get; set; }
        ReadingType ReadingType { get; set; }
        T Value { get; set; }
    }
}