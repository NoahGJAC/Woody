using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Enums;
using Woody.Interfaces;

namespace Woody.Models
{
    public class User : INotifyPropertyChanged, IHasUKey
    {
        public string Key { get; set; }
        public string Uid { get; set; }
        public string Username { get; set; }
        public UserType UserType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
