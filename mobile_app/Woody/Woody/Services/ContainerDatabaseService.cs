using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Interfaces;

namespace Woody.Services
{
    // ContainerDatabaseService will most likely use CosmosDB, while UserDatabaseService will use firebase
    // https://github.com/Azure/azure-cosmos-dotnet-v3 ?
    public class ContainerDatabaseService<T> : IDataStore<T> where T : class, IController
    {
        public ObservableCollection<T> Items => throw new NotImplementedException();

        public Task<bool> AddItemsAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemsAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateItemsAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}
