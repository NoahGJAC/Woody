using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Enums;

namespace Woody.Interfaces
{
    public interface IReading
    {
        ReadingUnit Unit { get; set; }
        ReadingType ReadingType { get; set; }
    }
    public interface IReading<T> : IReading
    {
        T Value { get; set; }
        DateTime TimeStamp { get; set; }
    }
}