using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Enums;

namespace Woody.Interfaces
{
    public interface ICommand<T>
    {
        CommandType CommandType { get; set; }
        T Value { get; set; }
    }
}
