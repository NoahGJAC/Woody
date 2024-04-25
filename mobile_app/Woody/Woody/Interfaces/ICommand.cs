using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Enums;

namespace Woody.Interfaces
{
    public interface ICommand
    {
        CommandType CommandType { get; set; }
    }
    public interface ICommand<T>: ICommand
    {
        T Value { get; set; }
    }
}
