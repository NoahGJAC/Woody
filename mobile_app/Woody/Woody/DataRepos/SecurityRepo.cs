using System.ComponentModel;
using Woody.Models;
using Woody.Services;

namespace Woody.DataRepos
{
    public class SecurityRepo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public SecurityController SecurityController { get; set; }

        private ContainerDatabaseService<SecurityController> securityDb;

        public ContainerDatabaseService<SecurityController> SecurityDb
        {
            get { return securityDb; }
        }

        public SecurityRepo()
        {
            securityDb = new ContainerDatabaseService<SecurityController>();
        }
    }
}
