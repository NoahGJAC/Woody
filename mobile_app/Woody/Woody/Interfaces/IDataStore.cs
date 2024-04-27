using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Woody.Interfaces
{
    internal interface IDataStore<T>
    {
        /* taken from lab6
        Task<bool> AddItemsAsync(T item);
        Task<bool> UpdateItemsAsync(T item);
        Task<bool> DeleteItemsAsync(T item);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        public ObservableCollection<T> Items { get; }
        */
    }
}
