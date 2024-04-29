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
    /// <summary>
    /// Represents a user in the system with properties and methods for property change notification.
    /// </summary>
    public class User : INotifyPropertyChanged, IHasUKey
    {
        /// <summary>
        /// Gets or sets the unique key of the user.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user.
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the type of the user.
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
