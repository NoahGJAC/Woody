using ClassKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Interfaces;

namespace Woody.Services
{
    // ContainerDatabaseService will most likely use CosmosDB, while UserDatabaseService will use firebase
    public class ContainerDatabaseService<T> : IDataStore<T> where T : class, IController
    {
    }
}
