using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Woody.Enums;
using Woody.Interfaces;

namespace Woody.Models
{
    public class Command<T> : ICommand<T>
    {
        public T Value { get; set; }
        public CommandType CommandType { get; set; }
        public SubSystemType SubSystem { get; set; }

        public Command(T value, CommandType commandType, SubSystemType subSystem)
        {
            Value = value;
            CommandType = commandType;
            SubSystem = subSystem;
        }
    }
}
