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
    /// <summary>
    /// Represents a service for managing data in a container database, specifically designed for objects implementing <see cref="IController"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items stored in the container database, which must implement <see cref="IController"/>.</typeparam>
    public class ContainerDatabaseService<T> : IDataStore<T> where T : class, IController
    {
        /// <summary>
        /// Gets an observable collection of items managed by the container database service.
        /// </summary>
        /// <remarks>
        /// This property is not implemented and will throw a <see cref="NotImplementedException"/>.
        /// </remarks>
        public ObservableCollection<T> Items => throw new NotImplementedException();

        /// <summary>
        /// Asynchronously adds an item to the container database.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, returning true if the item was added successfully, otherwise false.</returns>
        /// <remarks>
        /// This method is not implemented and will throw a <see cref="NotImplementedException"/>.
        /// </remarks>
        public Task<bool> AddItemsAsync(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously deletes an item from the container database.
        /// </summary>
        /// <param name="item">The item to delete.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, returning true if the item was deleted successfully, otherwise false.</returns>
        /// <remarks>
        /// This method is not implemented and will throw a <see cref="NotImplementedException"/>.
        /// </remarks>
        public Task<bool> DeleteItemsAsync(T item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously retrieves items from the container database.
        /// </summary>
        /// <param name="forceRefresh">A boolean indicating whether to force a refresh of the data store.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, returning an <see cref="IEnumerable{T}"/> of items.</returns>
        /// <remarks>
        /// This method is not implemented and will throw a <see cref="NotImplementedException"/>.
        /// </remarks>
        public Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously updates an item in the container database.
        /// </summary>
        /// <param name="item">The item to update.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation, returning true if the item was updated successfully, otherwise false.</returns>
        /// <remarks>
        /// This method is not implemented and will throw a <see cref="NotImplementedException"/>.
        /// </remarks>
        public Task<bool> UpdateItemsAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}
